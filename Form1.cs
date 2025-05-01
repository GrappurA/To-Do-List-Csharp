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

		const int percentageToGetAStar = 50;

		public mainForm()
		{
			InitializeComponent();
			addButton.Enabled = false;
			deleteButton.Enabled = false;
			SaveButton.Enabled = false;

			infoTextBox.ReadOnly = true;

			gridView.RowHeadersVisible = false;
			gridView.AllowUserToAddRows = false;
			gridView.MultiSelect = false;
			gridView.SelectionChanged += gridView_SelectionChanged;
			gridView.DataError += gridView_DataError;
			gridView.CurrentCellDirtyStateChanged += gridView_CurrentCellDirtyStateChanged;

			taskList = new TaskList();
			loadedUser = new User();

			//OpenLatestFile();
			HideBars();

		}
		private async void LoadUserDB()
		{
			UsersDBContext dBContext = new UsersDBContext();
			loadedUser = new User();

			await dBContext.Database.EnsureCreatedAsync();

			try
			{
				loadedUser = await dBContext.users.OrderByDescending(u => u.Id).FirstOrDefaultAsync();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error while loading users: " + ex.Message);
				loadedUser = new User();
			}
		}

		private async void SaveUserDBChanges()
		{
			UsersDBContext dbContext = new UsersDBContext();

			dbContext.users.Update(loadedUser);

			try
			{
				await dbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error saving users: " + ex.Message);
			}

		}

		private async void LoadListDB()
		{
			TaskListDBContext dBContext = new TaskListDBContext();

			await dBContext.Database.EnsureCreatedAsync();
			try
			{
				var latestTaskList = await dBContext.lists
					.Include(tl => tl.GetList()) // Load related tasks (if using EF Core with navigation property)
					.OrderByDescending(tl => tl.dateTime)
					.FirstOrDefaultAsync();

				taskList.taskList = latestTaskList.taskList ?? new List<Task>();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error while loading users: " + ex.Message);
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
			LoadUserDB();
			LoadListDB();

			addButton.Enabled = true;
			deleteButton.Enabled = true;
			SaveButton.Enabled = true;
			this.Text = taskList.Name;

			infoTextBox.Text = taskList.DonePercentage.ToString();

			PrintStarsCount(loadedUser.stars.Count.ToString());

			UpdateGridView();
		}

		private BindingSource _taskListSource;
		private void UpdateGridView()
		{
			if (_taskListSource == null)
			{
				_taskListSource = new BindingSource();
			}
			_taskListSource.DataSource = taskList.taskList;
			gridView.DataSource = _taskListSource;
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

			gridView.ResumeLayout();
			//
			//gridView.Rows[1].Height = 100;
		}

		//environment
		int tempIdCounter = 0;
		private void addButton_Click(object sender, EventArgs e)//ABC
		{
			if (taskList != null)
			{
				DateTime tommorow = DateTime.Today.AddDays(1);
				Task newTask = new Task("", false, tommorow);
				newTask.Id = ++tempIdCounter;

				taskList.taskList.Add(newTask);

				_taskListSource.ResetBindings(false);//fixes index -1 doesnt have a value exception
				UpdateGridView();

				deleteButton.Enabled = taskList.Count() > 0;
				infoTextBox.Text = calculatePercentageByList(taskList).ToString();

				AnimateButton(addButton, Color.Green, 35);
			}
		}

		private void createToolStripMenuItem_Click(object sender, EventArgs e)//CreateT
		{
			while (true)
			{
				using (PickDateForm pickDateForm = new PickDateForm())
				{
					using (GetListNameForm form = new GetListNameForm())
					{
						if (form.ShowDialog() == DialogResult.OK)
						{
							taskList.Clear();
							UpdateGridView();
							taskList.Name = form.enteredName;
							if (taskList.Name == "L")
							{
								form.Close();
								break;
							}
							else if (!string.IsNullOrEmpty(taskList.Name))
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
						else
						{
							MessageBox.Show("Something went wrong while naming the list...");
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

		private void deleteButton_Click(object sender, EventArgs e)
		{
			if (taskList.Count() > 0)
			{
				taskList.taskList.RemoveAt(gridView.RowCount - 1);
				UpdateGridView();
			}
			deleteButton.Enabled = taskList.Count() > 0;
			SaveButton.Enabled = true;

			infoTextBox.Text = calculatePercentageByList(taskList).ToString();
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
						dbContext.lists.Add(currentProgress);
					}
					dbContext.SaveChanges();
				}

				if (loadedUser != null)
				{
					loadedUser.SetStarList(loadedUser.stars);
				}
				else
				{
					User currentProgress = new User();
					currentProgress.SetStarList(loadedUser.stars);
					loadedUser = currentProgress;
				}
			}
			SaveUserDBChanges();
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
			//using (var progress = new ProgressDBContext())
			//{
			//	foreach (var p in progress.progresses)
			//	{
			//		MessageBox.Show(p.ToString() + $"\n {p.Id}\n{p.DonePercentage}\n{p.dateTime.Date.ToString("dd/MM/yyyy")}");
			//	}
			//}
		}

		private void listInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//using (var progress = new ProgressDBContext())
			//{
			//	using (var transaction = progress.Database.BeginTransaction())
			//	{
			//		try
			//		{
			//			// Remove all records from the progresses table
			//			progress.progresses.RemoveRange(progress.progresses);
			//			progress.SaveChanges();
			//
			//			// Reset the auto-incrementing ID (set it back to 1)
			//			progress.Database.ExecuteSqlRaw("DELETE FROM SQLITE_SEQUENCE WHERE NAME = 'Progresses'");
			//
			//			// Commit the transaction
			//			transaction.Commit();
			//		}
			//		catch (Exception ex)
			//		{
			//			// Rollback the transaction in case of error
			//			transaction.Rollback();
			//			MessageBox.Show($"Error clearing database: {ex.Message}");
			//		}
			//	}
			//}
		}

		private void userInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var user = new UsersDBContext())
			{
				if (!File.Exists("C:\\Users\\Nika\\source\\repos\\ToDoList_C#\\bin\\Debug\\net8.0-windows\\Users.db"))
				{
					MessageBox.Show("users data base not found");
				}
				else
					File.Delete("C:\\Users\\Nika\\source\\repos\\ToDoList_C#\\bin\\Debug\\net8.0-windows\\Users.db");

			}
		}

		private void gridView_SelectionChanged(object sender, EventArgs e)
		{
			// now you never see e.RowIndex here
			deleteButton.Enabled = (gridView.SelectedRows.Count > 0);
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
				taskList.DonePercentage = calculatePercentageByList(taskList);
				infoTextBox.Text = taskList.DonePercentage.ToString();

			}

		}
	}
}