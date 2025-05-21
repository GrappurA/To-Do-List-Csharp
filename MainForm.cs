using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Web.WebView2.WinForms;

namespace ToDoList_C_
{
	public partial class mainForm : Form
	{
		TaskList taskList;
		User loadedUser;

		const int percentageToGetAStar = 65;

		public mainForm()
		{
			InitializeComponent();
			this.Load += mainForm_Load;

			gridView.DataError += gridView_DataError;
			gridView.CurrentCellDirtyStateChanged += gridView_CurrentCellDirtyStateChanged;
			gridView.CellClick += gridView_CellClick;
		}

		private async void mainForm_Load(object? sender, EventArgs e)
		{
			taskList = new TaskList();
			loadedUser = new User();

			addButton.Enabled = false;
			deleteButton.Enabled = false;
			SaveButton.Enabled = false;
			infoTextBox.ReadOnly = true;

			gridView.RowHeadersVisible = false;
			gridView.AllowUserToAddRows = false;
			gridView.MultiSelect = false;

			fireFrameGifBP.SendToBack();

			await LoadUserDBAsync();
			await LoadListDBAsync();
			await OpenLatestFile();

			SetupChart();

			await showingStarsWV2.EnsureCoreWebView2Async();
			SetupShowingStarsWebView();
			await showingFireWV2.EnsureCoreWebView2Async();
			SetupShowingFireWebView();
			await showLatestStarWV.EnsureCoreWebView2Async();
			SetupShowLatestStarWebView();
			await showDayStreak.EnsureCoreWebView2Async();
			SetupShowDayStreakWebView();
			await showMaxStreakWV.EnsureCoreWebView2Async();
			SetupShowMaxStreakWebView();

			UpdateGridView();
			await HideBars();
		}

		private async Task SetupChart()
		{
			percentageToDaysChart.Series.Clear();
			percentageToDaysChart.ChartAreas.Clear();

			ChartArea area = new("MainArea");
			percentageToDaysChart.ChartAreas.Add(area);

			Series series = new Series("Tasks Done")
			{
				//YValuesPerPoint = 32,
				ChartType = SeriesChartType.Spline,
				MarkerStyle = MarkerStyle.Circle,
				MarkerColor = Color.Red,
				IsValueShownAsLabel = true,// shows numbers above columns
				MarkerSize = 6,
			};
			percentageToDaysChart.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM";

			List<DateTime> dates1 = new List<DateTime>();
			DateTime today = DateTime.Today;
			for (int i = 7; i > 0; i--)
			{
				DateTime day = today.AddDays(-i);
				dates1.Add(day);
			}

			List<int> percentages;
			using (TaskListDBContext dbContext = new())
			{
				percentages = await dbContext.lists.OrderByDescending(l => l.CreationDate)
					.Select(l => l.DonePercentage)
					.Take(7)
					.ToListAsync();
			}

			for (int i = 0; i < percentages.Count; i++)
			{
				series.Points.AddXY(dates1[i], percentages[i]);
			}

			percentageToDaysChart.Series.Add(series);
		}

		private async Task LoadUserDBAsync()
		{
			using (var db = new UsersDBContext())
			{
				await db.Database.EnsureCreatedAsync();

				try
				{
					// await a Task<T>, so the containing method returns Task
					loadedUser = await db.users
						.Include(u => u.stars)
						.FirstOrDefaultAsync() ?? new User();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error while loading users: " + ex.Message);
					loadedUser = new User();
				}
			}
		}

		#region webviewStuff
		private void SetupShowingStarsWebView()
		{
			showingStarsWV2.CoreWebView2.NavigationCompleted += async (s, e) =>
			{
				await DisableScrollAsync(showingStarsWV2);
			};
		}

		private void SetupShowingFireWebView()
		{
			showingFireWV2.CoreWebView2.NavigationCompleted += async (s, e) =>
			{
				await DisableScrollAsync(showingFireWV2);
			};
		}

		private void SetupShowLatestStarWebView()
		{
			showLatestStarWV.CoreWebView2.NavigationCompleted += async (s, e) =>
			{
				await DisableScrollAsync(showLatestStarWV);
			};
		}

		private void SetupShowDayStreakWebView()
		{
			showDayStreak.CoreWebView2.NavigationCompleted += async (s, e) =>
			{
				await DisableScrollAsync(showDayStreak);
			};
		}

		private void SetupShowMaxStreakWebView()
		{
			showMaxStreakWV.CoreWebView2.NavigationCompleted += async (s, e) =>
			{
				await DisableScrollAsync(showMaxStreakWV);
			};
		}

		private async Task DisableScrollAsync(Microsoft.Web.WebView2.WinForms.WebView2 webView)
		{
			if (webView != null && webView.CoreWebView2 != null)
			{
				await webView.ExecuteScriptAsync(@"
			document.documentElement.style.overflow = 'hidden';
			document.body.style.overflow = 'hidden';
		");
			}
		}

		private async Task HideBars()
		{
			await DisableScrollAsync(showingFireWV2);
			await DisableScrollAsync(showingStarsWV2);
			await DisableScrollAsync(showLatestStarWV);
			await DisableScrollAsync(showDayStreak);
			await DisableScrollAsync(showMaxStreakWV);
		}
		#endregion

		private async Task CalculateDaysInARow()
		{
			using (TaskListDBContext dbContext = new())
			{
				await dbContext.Database.EnsureCreatedAsync();
				var statuses = await dbContext.lists.Select(l => l.GotStar).ToArrayAsync();
				int currentStreak = 0;
				//calculating currnet streak
				if (statuses[statuses.Length - 1] != false)
				{
					currentStreak++;
					for (int i = statuses.Length - 2; i >= 0; i--)
					{
						if (statuses[i] == true)
						{
							currentStreak++;
						}
						else
						{
							break;
						}
					}
				}
				else
				{
					currentStreak = 0;
				}

				//calculating max streak
				int maxStreak = 0;
				int counter = 0;
				for (int i = 0; i < statuses.Length; i++)
				{
					if (statuses[i] == true)
					{
						for (int j = i + 1; j < statuses.Length; j++)
						{
							if (statuses[j] == true)
							{
								counter++;
								if (counter > maxStreak)
								{
									maxStreak = counter;
								}
							}
							else
							{
								//maxStreak = counter;
								counter = 0;
							}
						}
						counter = 0;

					}
				}
				loadedUser.daysInARow = currentStreak;
				loadedUser.MaxDaysInARow = maxStreak + 1;
			}
		}

		private async Task SaveUserDBChanges()
		{
			using (UsersDBContext dBContext = new UsersDBContext())
			{
				dBContext.Database.EnsureCreated();

				if (dBContext.users.Count() > 0)
				{
					User existingUser = await dBContext.users.OrderByDescending(u => u.Id).FirstOrDefaultAsync();

					if (loadedUser != null)
					{
						existingUser.CurrentList = loadedUser.CurrentList;
						existingUser.CurrentListId = loadedUser.CurrentListId;
						existingUser.stars = loadedUser.stars;
						existingUser.averageTasksDone = loadedUser.averageTasksDone;
						existingUser.daysInARow = loadedUser.daysInARow;

					}
					await dBContext.SaveChangesAsync();
				}
				else
				{
					User firstUser = new User(1);
					dBContext.users.Add(firstUser);
					await dBContext.SaveChangesAsync();
				}
			}

		}

		private async Task LoadListDBAsync()
		{
			TaskListDBContext dBContext = new TaskListDBContext();

			await dBContext.Database.EnsureCreatedAsync();
			try
			{
				var latestList = await dBContext.lists
				.Include(tl => tl.taskList)
				.OrderByDescending(tl => tl.CreationDate)
				.FirstOrDefaultAsync();
				if (latestList == null)
				{
					latestList = new();
				}
				taskList.taskList = latestList.taskList?.Select(t => new ToDoTask(t)).ToList()
					?? new List<ToDoTask>();
				taskList.CreationDate = latestList.CreationDate;
				taskList.DonePercentage = latestList.DonePercentage;
				taskList.GotStar = latestList.GotStar;
				taskList.Id = latestList.Id;
				taskList.Name = latestList.Name;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error while loading lists: " + ex.Message);
				taskList = new TaskList();
			}
		}

		//additional funcs

		async void AnimateButton(Button button, Color color, int delay)
		{
			Color originalColor = button.BackColor;
			button.BackColor = color;
			await System.Threading.Tasks.Task.Delay(delay);
			button.BackColor = originalColor;
		}

		private int CalculateDonePercentage(TaskList taskList)
		{
			if (taskList == null || taskList.Count() == 0) { return 0; }

			int res = 0;
			int oneTaskPercentage = 100 / taskList.Count();
			int counter = 0;

			foreach (var task in taskList.taskList)
			{
				if (task.Status == true)
				{
					res += oneTaskPercentage;
					counter++;

				}
			}

			if (counter == taskList.Count())
			{
				res = 100;
			}
			if (taskList.Count() % 2 == 0 && counter == taskList.Count() / 2)
			{
				res = 50;
			}

			if (res >= percentageToGetAStar)
			{
				SetFireEmoji();
				HideBars();

				if (!loadedUser.stars.Any(s => s.ListName == taskList.Name))
				{
					taskList.GotStar = true;
					Star smallStar = new Star
					{
						Size = 1,
						earnDate = DateTime.Today,
						ListName = taskList.Name,
					};
					loadedUser.stars.Add(smallStar);
					PrintStarsCount(loadedUser.stars.Count.ToString());
				}
			}
			else
			{
				setDefaultEmoji();
				HideBars();
			}
			return res;
		}

		async void SetFireEmoji()
		{
			await showingFireWV2.EnsureCoreWebView2Async();
			showingFireWV2.NavigateToString("<html><body style='font-size:12px;'>🔥</body></html>");
		}

		async void setDefaultEmoji()
		{
			await showingFireWV2.EnsureCoreWebView2Async();
			showingFireWV2.NavigateToString("<html><body style='font-size:12px;'></body></html>");
		}

		async Task PrintStarsCount(string count)
		{
			await showingStarsWV2.EnsureCoreWebView2Async();
			showingStarsWV2.NavigateToString($"" +
				$"<html>" +
				$"<body style='" +
				$"font-family: Segoe UI, sans-serif;" +
				$"font-size:23px;'>" +
				$"You have {count} Stars" +
				$"</body>" +
				$"</html>");
		}

		async Task PrintLatestStar(string date)
		{
			await showLatestStarWV.EnsureCoreWebView2Async();
			showLatestStarWV.NavigateToString($"" +
				$"<html>" +
				$"<body style='" +
				$"font-family: Segoe UI, sans-serif;" +
				$"font-size:23px;'>" +
				$"Latest star: {date}" +
				$"</body>" +
				$"</html>");
		}

		async Task PrintCurrentStreak(string streak)
		{
			await showDayStreak.EnsureCoreWebView2Async();
			showDayStreak.NavigateToString($"" +
				$"<html>" +
				$"<body style='" +
				$"font-family: Segoe UI, sans-serif;" +
				$"font-size:23px;'>" +
				$"Your current day-streak: {streak}" +
				$"</body>" +
				$"</html>");
		}

		async Task PrintMaxStreak(string streak)
		{
			await showMaxStreakWV.EnsureCoreWebView2Async();
			showMaxStreakWV.NavigateToString($"" +
				$"<html>" +
				$"<body style='" +
				$"font-family: Segoe UI, sans-serif;" +
				$"font-size:23px;'>" +
				$"Biggest streak: {streak}" +
				$"</body>" +
				$"</html>");
		}

		private async Task OpenLatestFile()
		{
			if (taskList.taskList.Count != 0)
			{
				addButton.Enabled = true;
				deleteButton.Enabled = true;
				SaveButton.Enabled = true;
				this.Text = taskList.Name;
				infoTextBox.Text = taskList.DonePercentage.ToString();

				await RefreshStatisticsInfo();

				UpdateGridView();
			}
			else
			{
				this.Text = "To-Do List";
				addButton.Enabled = false;
				deleteButton.Enabled = false;
				SaveButton.Enabled = false;
				UpdateGridView();
			}
		}

		private async Task RefreshStatisticsInfo()
		{
			await CalculateDaysInARow();

			await PrintStarsCount(loadedUser.stars.Count.ToString());

			var latestStar = loadedUser.stars
				.OrderByDescending(s => s.earnDate)
			.FirstOrDefault();
			if (latestStar != null)
			{
				await PrintLatestStar(latestStar.earnDate.ToShortDateString());
			}
			else
			{
				await PrintLatestStar("*missing*");
			}

			await PrintCurrentStreak(loadedUser.daysInARow.ToString());

			await PrintMaxStreak(loadedUser.MaxDaysInARow.ToString());

		}

		private BindingSource _taskListSource;
		private void UpdateGridView()
		{
			if (_taskListSource == null)
			{
				_taskListSource = new BindingSource();
				gridView.DataSource = _taskListSource;
			}
			_taskListSource.DataSource = null;
			_taskListSource.DataSource = taskList.GetList();

			gridView.Columns[0].ReadOnly = true;
			AdjustGridViewSizesLooks();
		}

		private void AdjustGridViewSizesLooks()
		{
			gridView.SuspendLayout();

			gridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			gridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			gridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

			gridView.Columns[0].Width = 30;
			gridView.Columns[2].Width = 70;

			gridView.Columns[0].HeaderText = "ID";
			gridView.ResumeLayout();
		}

		//environment
		private void addButton_Click(object sender, EventArgs e)//ABC
		{
			if (taskList != null)
			{
				DateTime tommorow = DateTime.Today.AddDays(1);
				ToDoTask newTask = new ToDoTask("", false, tommorow);

				newTask.Position = taskList.Count() + 1;

				taskList.taskList.Add(newTask);

				_taskListSource.ResetBindings(false);//fixes index -1 doesnt have a value exception
				UpdateGridView();

				deleteButton.Enabled = taskList.Count() > 0;
				UpdateProgressUI();

				AnimateButton(addButton, Color.Green, 35);
			}
		}

		private List<string> LoadAllListNames()
		{
			List<string> list = new List<string>();
			using (TaskListDBContext dBContext = new TaskListDBContext())
			{
				list.AddRange(dBContext.lists.Select(l => l.Name).ToList());
			}
			return list;
		}

		private bool IsUsedName(string s)
		{
			List<string> list = LoadAllListNames();
			if (list.Contains(s))
			{
				return true;
			}
			return false;
		}

		private void ResetTaskList()
		{
			taskList.taskList.Clear();
			taskList.GotStar = false;
			taskList.DonePercentage = 0;
			taskList.Name = "default_name";
			taskList.CreationDate = DateTime.MinValue;
		}

		private void createToolStripMenuItem_Click(object sender, EventArgs e)//CreateT
		{
			using (GetListNameForm getListNameForm = new GetListNameForm())
			{
				while (getListNameForm.DialogResult != DialogResult.OK)
				{
					if (getListNameForm.DialogResult == DialogResult.Cancel)
					{
						return;
					}
					else if (getListNameForm.ShowDialog() == DialogResult.OK)
					{
						if (IsUsedName(getListNameForm.enteredName))
						{
							MessageBox.Show("Invalid Name");
							continue;
						}
						if (taskList != null)
						{
							ResetTaskList();
						}
						UpdateGridView();
						taskList.Name = getListNameForm.enteredName;

						if (!string.IsNullOrEmpty(taskList.Name))
						{
							addButton.Enabled = true;
							deleteButton.Enabled = true;
							SaveButton.Enabled = true;

							UpdateGridView();

							this.Text = taskList.Name;
							taskList.CreationDate = DateTime.Now;

							break;
						}
						else
						{
							MessageBox.Show("Invalid name");
						}
					}

				}
			}

		}

		private void deleteButton_Click(object sender, EventArgs e)//dbcc
		{
			if (taskList.Count() > 0)
			{
				int selectedIndex;
				try
				{
					selectedIndex = gridView.SelectedCells[0].RowIndex;
				}
				catch (Exception)
				{
					selectedIndex = taskList.Count() - 1;
				}
				if (selectedIndex > -1 && selectedIndex < taskList.Count())
				{
					var taskToRemove = taskList.taskList[selectedIndex];

					//using (TaskListDBContext dbContext = new())
					//{
					//	dbContext.Attach(taskToRemove);
					//	dbContext.Remove(taskToRemove);
					//	dbContext.SaveChanges();
					//}

					taskList.taskList.RemoveAt(selectedIndex);
					UpdateGridView();
				}
				else
				{
					taskList.taskList.RemoveAt(taskList.Count() - 1);
				}
			}
			deleteButton.Enabled = taskList.Count() > 0;
			SaveButton.Enabled = true;

			UpdateProgressUI();
			AnimateButton(deleteButton, Color.DarkRed, 60);
		}

		private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			SaveButton.Enabled = false;
			addButton.Enabled = false;
			deleteButton.Enabled = false;
			taskList.Clear();
			taskList = new();
			this.Text = "To-Do List";
			UpdateGridView();
		}

		private async void SaveButton_Click(object sender, EventArgs e)//SBC
		{
			if (taskList.taskList == null)
			{
				this.Name = "To-Do List";
				MessageBox.Show("TaskList is null");
				return;
			}
			else
			{
				using (var dbContext = new TaskListDBContext())
				{
					DateTime today = DateTime.Today;
					DateTime tommorow = today.AddDays(1);

					TaskList existingList = dbContext.lists.FirstOrDefault(p => p.CreationDate >= today && p.CreationDate < tommorow && p.Name == taskList.Name);

					if (existingList != null)
					{
						existingList.taskList = taskList.taskList;
						existingList.Name = taskList.Name;
						existingList.CreationDate = taskList.CreationDate;
						existingList.DonePercentage = taskList.DonePercentage;
						existingList.GotStar = taskList.GotStar;
					}
					else
					{
						TaskList currentProgress = new TaskList(DateTime.Now, taskList.DonePercentage);
						currentProgress.taskList = taskList.taskList;
						currentProgress.Name = taskList.Name;
						currentProgress.CreationDate = taskList.CreationDate;
						currentProgress.DonePercentage = taskList.DonePercentage;
						currentProgress.GotStar = taskList.GotStar;
						dbContext.lists.Add(currentProgress);
					}
					dbContext.SaveChanges();
				}
			}
			await SaveUserDBChanges();
			UpdateGridView();
			AnimateButton(SaveButton, Color.ForestGreen, 60);
			await RefreshStatisticsInfo();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (openListForm form = new())
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					taskList = new TaskList();
					taskList.SetList(form.wrapper.taskList);
					loadedUser = form.wrapper.user;
					this.Text = form.wrapper.taskList.Name;
					CalculateDonePercentage(taskList);

					addButton.Enabled = true;
					deleteButton.Enabled = taskList.taskList.Count > 0;
					SaveButton.Enabled = true;

					UpdateGridView();
				}
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//using (folderBrowserDialog2)
			//{
			//	folderBrowserDialog2.InitialDirectory = path;
			//	using (approveClosingList form = new approveClosingList())
			//	{
			//		if (folderBrowserDialog2.ShowDialog() == DialogResult.OK && form.ShowDialog() == DialogResult.OK)
			//		{
			//			UpdateGridView();
			//
			//			string toDeleteDirectoryPath = folderBrowserDialog2.SelectedPath;
			//			Directory.Delete(toDeleteDirectoryPath, true);
			//		}
			//	}
			//
			//}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			using (var progress = new TaskListDBContext())
			{
				foreach (var p in progress.lists)
				{
					MessageBox.Show(p.ToString() + $"\n {p.Id}\n{p.DonePercentage}\n{p.CreationDate.Date.ToString("dd/MM/yyyy")}");
				}
			}
		}

		private void gridView_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == 0 || taskList.taskList.Count() < 1)
			{
				return;
			}

			if (gridView.SelectedCells != null && gridView.Columns[e.ColumnIndex].Index == 3)
			{
				using (PickDateForm pickDateForm = new PickDateForm())
				{
					if (pickDateForm.ShowDialog() == DialogResult.OK)
					{
						int selectedIndex = gridView.SelectedCells[0].RowIndex;
						taskList.taskList[selectedIndex].DueDate = pickDateForm.enteredDate;
						UpdateGridView();
					}
				}
			}
		}

		private void gridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			// prevent the default “pop-up” and crash
			e.ThrowException = false;
		}

		void gridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			int DoneColumnIndex = 2;
			if (gridView.CurrentCell != null && gridView.CurrentCell.ColumnIndex == DoneColumnIndex)
			{
				gridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
				UpdateProgressUI();
			}
		}

		private void UpdateProgressUI()
		{
			taskList.DonePercentage = CalculateDonePercentage(taskList);
			infoTextBox.Text = taskList.DonePercentage.ToString();
		}


	}
}