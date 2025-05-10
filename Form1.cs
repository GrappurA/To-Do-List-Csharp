using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;

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

			gridView.SelectionChanged += gridView_SelectionChanged;
			gridView.DataError += gridView_DataError;
			gridView.CurrentCellDirtyStateChanged += gridView_CurrentCellDirtyStateChanged;
			gridView.CellClick += gridView_CellClick;
		}

		private async void mainForm_Load(object? sender, EventArgs e)
		{
			addButton.Enabled = false;
			deleteButton.Enabled = false;
			SaveButton.Enabled = false;
			infoTextBox.ReadOnly = true;

			gridView.RowHeadersVisible = false;
			gridView.AllowUserToAddRows = false;
			gridView.MultiSelect = false;

			taskList = new TaskList();
			loadedUser = new User();

			LoadUserDBAsync();
			LoadListDBAsync();
			OpenLatestFile();

			UpdateGridView();
			HideBars();
		}

		private async void LoadUserDBAsync()
		{
			UsersDBContext dBContext = new UsersDBContext();
			loadedUser = new User();

			await dBContext.Database.EnsureCreatedAsync();

			try
			{
				loadedUser = await dBContext.users.FirstOrDefaultAsync();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error while loading users: " + ex.Message);
				loadedUser = new User();
			}
		}

		private async void SaveUserDBChanges()
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

		private async void LoadListDBAsync()
		{
			TaskListDBContext dBContext = new TaskListDBContext();

			await dBContext.Database.EnsureCreatedAsync();
			try
			{
				var latestList = await dBContext.lists
				.Include(tl => tl.taskList)
				.OrderByDescending(tl => tl.dateTime)
				.FirstOrDefaultAsync();
				if (latestList == null)
				{
					latestList = new();
				}
				taskList.taskList = latestList.taskList ?? new List<Task>();
				taskList.dateTime = latestList.dateTime;
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
		private async void HideBars()
		{
			await showingFireWV2.EnsureCoreWebView2Async();
			await showingStarsWV2.EnsureCoreWebView2Async();

			await showingFireWV2.ExecuteScriptAsync(@"
			document.documentElement.style.overflow = 'hidden';
			document.body.style.overflow = 'hidden';
			");
			await showingStarsWV2.ExecuteScriptAsync(@"
			document.documentElement.style.overflow = 'hidden';
			document.body.style.overflow = 'hidden';
			");
		}

		async void AnimateButton(Button button, Color color, int delay)
		{
			Color originalColor = button.BackColor;
			button.BackColor = color;
			await System.Threading.Tasks.Task.Delay(delay);
			button.BackColor = originalColor;
		}

		private int calculatePercentageByList(TaskList taskList)
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

		async void PrintStarsCount(string count)
		{
			await showingStarsWV2.EnsureCoreWebView2Async();
			showingStarsWV2.NavigateToString($"<html><body style='font-size:12px;'>You have {count} Stars</body></html>");
		}

		private async void OpenLatestFile()
		{
			if (taskList.taskList.Count != 0)
			{
				addButton.Enabled = true;
				deleteButton.Enabled = true;
				SaveButton.Enabled = true;
				this.Text = taskList.Name;
				infoTextBox.Text = taskList.DonePercentage.ToString();
				PrintStarsCount(loadedUser.stars.Count.ToString());
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

		private BindingSource _taskListSource;
		private void UpdateGridView()
		{
			if (_taskListSource == null)
			{
				_taskListSource = new BindingSource();
				gridView.DataSource = _taskListSource;
			}
			_taskListSource.DataSource = null;
			_taskListSource.DataSource = taskList.taskList;

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
				Task newTask = new Task("", false, tommorow);

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
						taskList.Clear();
						UpdateGridView();
						taskList.Name = getListNameForm.enteredName;


						if (!string.IsNullOrEmpty(taskList.Name))
						{
							addButton.Enabled = true;
							deleteButton.Enabled = true;
							SaveButton.Enabled = true;

							UpdateGridView();

							this.Text = taskList.Name;
							taskList.dateTime = DateTime.Today;

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


		//private void gridView_SelectionChanged(object sender, DataGridViewCellEventArgs e)
		//{
		//	if (gridView.Rows.Count == 0)
		//	{
		//		return;
		//	}
		//	if (gridView.SelectedRows.Count > 0)
		//	{
		//		int index = gridView.SelectedRows[0].Index;
		//		if (index >= 0 && index < taskList.Count())
		//		{
		//			deleteButton.Enabled = true;
		//		}
		//	}
		//	else
		//	{
		//		deleteButton.Enabled = false;
		//	}
		//}

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
			UpdateGridView();
		}

		private void SaveButton_Click(object sender, EventArgs e)//SBC
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

					TaskList existingList = dbContext.lists.FirstOrDefault(p => p.dateTime >= today && p.dateTime < tommorow);

					if (existingList != null)
					{
						existingList.taskList = taskList.taskList;
						existingList.Name = taskList.Name;
						existingList.dateTime = taskList.dateTime;
						existingList.DonePercentage = taskList.DonePercentage;
						existingList.GotStar = taskList.GotStar;
					}
					else
					{
						TaskList currentProgress = new TaskList(DateTime.Now, taskList.DonePercentage);
						currentProgress.taskList = taskList.taskList;
						currentProgress.Name = taskList.Name;
						currentProgress.dateTime = taskList.dateTime;
						currentProgress.DonePercentage = taskList.DonePercentage;
						currentProgress.GotStar = taskList.GotStar;
						dbContext.lists.Add(currentProgress);
					}
					dbContext.SaveChanges();
				}
			}
			SaveUserDBChanges();
			UpdateGridView();
			AnimateButton(SaveButton, Color.ForestGreen, 60);
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				//openFileDialog.InitialDirectory = path;
				//openFileDialog.Multiselect = false;
				//openFileDialog.Filter = "Json Files (*.json)|*.json|All Files (*.*)| *.*";
				//
				//if (openFileDialog.ShowDialog() == DialogResult.OK)
				//{
				//	addButton.Enabled = true;
				//	deleteButton.Enabled = true;
				//	SaveButton.Enabled = true;
				//
				//	taskList.setPathToList(openFileDialog.FileName);
				//
				//	openedFileName = Path.GetFileNameWithoutExtension(taskList.GetPathToList());
				//
				//	string PathToCurrentDirectory = Path.GetDirectoryName(taskList.GetPathToList());
				//	Directory.SetLastAccessTime(taskList.GetPathToList(), DateTime.Now);
				//
				//	taskList.Clear();
				//	taskList.SetList(readFile<List<Task>>(taskList.GetPathToList()));
				//	infoTextBox.Text = calculatePercentageByList(taskList).ToString();
				//
				//	fileNameList = openedFilePath;
				//	UpdateGridView();
				//	this.Text = openedFileName;
				//}
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
					MessageBox.Show(p.ToString() + $"\n {p.Id}\n{p.DonePercentage}\n{p.dateTime.Date.ToString("dd/MM/yyyy")}");
				}
			}
		}

		private void listInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var progress = new TaskListDBContext())
			{
				using (var transaction = progress.Database.BeginTransaction())
				{
					try
					{
						// Remove all records from the progresses table
						progress.lists.RemoveRange(progress.lists);
						progress.SaveChanges();

						// Reset the auto-incrementing ID (set it back to 1)
						progress.Database.ExecuteSqlRaw("DELETE FROM SQLITE_SEQUENCE WHERE NAME = 'Progresses'");

						// Commit the transaction
						transaction.Commit();
					}
					catch (Exception ex)
					{
						// Rollback the transaction in case of error
						transaction.Rollback();
						MessageBox.Show($"Error clearing database: {ex.Message}");
					}
				}
			}
		}

		private void userInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var user = new UsersDBContext())
			{
				foreach (var u in user.users)
				{
					user.users.RemoveRange(user.users);
					user.SaveChanges();
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

		private void gridView_SelectionChanged(object sender, EventArgs e)
		{

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
			taskList.DonePercentage = calculatePercentageByList(taskList);
			infoTextBox.Text = taskList.DonePercentage.ToString();
		}
	}
}