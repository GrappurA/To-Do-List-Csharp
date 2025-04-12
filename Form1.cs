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
		ListInfo listInfo;
		UserInfo userInfo;

		const string path = "G:\\Main\\ToDoLists\\";
		string pathToAccountFile;
		string directoryPath;
		string fileNameList;
		string fileNameInfo;
		string listName;

		string openedFilePath;
		string openedFileName;

		const int percentageToGetAStar = 50;

		public mainForm()
		{
			InitializeComponent();
			addButton.Enabled = false;
			deleteButton.Enabled = false;
			SaveButton.Enabled = false;
			gridView.RowHeadersVisible = false;
			infoTextBox.ReadOnly = true;
			pathToAccountFile = path + "Info.json";

			taskList = new TaskList();
			listInfo = new ListInfo();
			userInfo = new UserInfo(1);

			OpenLatestFile();
			CreateFile(pathToAccountFile);


			folderBrowserDialog1.InitialDirectory = path;
			folderBrowserDialog1.Description = "Open A To Do List file";

			HideBars();

			gridView.CurrentCellDirtyStateChanged += gridView_CurrentCellDirtyStateChanged;
			calculatePercentageByList(taskList);

		}

		private async void mainForm_Load(object sender, EventArgs e)
		{
			PrintStarsCount();
		}

		//additional funcs
		private void CreateFile(string pathToFile)
		{
			if (!File.Exists(pathToFile))
			{
				File.Create(pathToFile).Close();
			}

		}

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
			listInfo = new ListInfo();

			if (taskList == null || taskList.Count() == 0) { return 0; }

			int oneTaskPercentage = 100 / taskList.Count();
			int counter = 0;

			foreach (var task in taskList.GetList())
			{
				if (task.Status)
				{
					listInfo.DonePercentage += oneTaskPercentage;
					counter++;
				}
			}

			if (counter == taskList.Count())
			{
				listInfo.DonePercentage = 100;
			}

			if (listInfo.DonePercentage >= percentageToGetAStar)
			{
				SetFireEmoji();
				HideBars();

				string currentListName = Path.GetFileNameWithoutExtension(taskList.GetPathToList());
				if (!userInfo.stars.Any(s => s.ListName == currentListName))
				{
					listInfo.GotStar = true;
					Star smallStar = new Star
					{
						Size = 1,
						earnDate = DateTime.Today,
						ListName = currentListName
					};
					userInfo.stars.Add(smallStar);

					var json = System.Text.Json.JsonSerializer.Serialize(listInfo, new JsonSerializerOptions { WriteIndented = true });

					if (!string.IsNullOrEmpty(taskList.GetPathToInfo()))
					{
						File.WriteAllText(taskList.GetPathToInfo(), json);
					}
					else
					{
						MessageBox.Show("Error: No file path set for task info.");
					}

				}
				else
				{
					return listInfo.DonePercentage;
				}

			}
			else
			{
				setDefaultEmoji();
				HideBars();
			}

			PrintStarsCount();
			return listInfo.DonePercentage;
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

		async void PrintStarsCount()
		{
			await showingStarsWV2.EnsureCoreWebView2Async();
			showingStarsWV2.NavigateToString($"<html><body style='font-size:12px;'>You have {userInfo.stars.Count} Stars</body></html>");
		}

		private void OpenLatestFile()
		{
			if (!Directory.Exists(path))
			{
				MessageBox.Show("Check the directory, 'path' does not exist");
				return;
			}
			else
			{
				var latestDir = new DirectoryInfo(path)
				.GetDirectories()
				.OrderByDescending(f => f.LastAccessTime)
				.FirstOrDefault();

				var latestListFile = latestDir?.GetFiles()
					.Where(f => !f.Name.Contains("Info")).FirstOrDefault();

				var latestInfoFile = latestDir?.GetFiles()
					.Where(f => f.Name.Contains("Info")).FirstOrDefault();

				string numberOfStars = readFile<string>(pathToAccountFile);

				if (latestListFile != null)
				{
					using (var progress = new ProgressDBContext())
					{
						progress.Database.EnsureCreated();
					}

					using (var user = new UsersDBContext())
					{
						user.Database.EnsureCreated();
						try
						{
							//userInfo.SetStarList(user.users.FirstOrDefault(p => p.Id == 1).stars);
						}
						catch (Exception e)
						{
							MessageBox.Show(e.Message);
						}
					}

					string latestFilePath = latestListFile.FullName;
					string formName = Path.GetFileNameWithoutExtension(latestFilePath);

					addButton.Enabled = true;
					deleteButton.Enabled = true;
					SaveButton.Enabled = true;
					this.Text = formName;

					taskList.Clear();
					taskList.SetList(readFile<List<Task>>(latestFilePath));

					var json = File.ReadAllText(latestInfoFile.FullName);

					try
					{
						listInfo = System.Text.Json.JsonSerializer.Deserialize<ListInfo>(json);
					}
					catch (Exception)
					{
						MessageBox.Show("open vs, bratha");
					}

					infoTextBox.Text = listInfo.DonePercentage.ToString();

					fileNameList = latestFilePath;
					taskList.setPathToInfo(latestInfoFile.FullName);
					taskList.setPathToList(latestListFile.FullName);

					PrintStarsCount();
					UpdateGridView();
				}
			}
		}

		private void UpdateGridView()
		{
			if (taskList == null) taskList.SetList(new List<Task>());
			gridView.DataSource = null;
			gridView.DataSource = taskList.GetList();
			AdjustGridViewSizesLooks();
			gridView.Refresh();
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

		private static T readFile<T>(string filePath)
		{
			return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath));
		}

		private void CreateDirectory(string directoryPath)
		{
			if (Directory.Exists(directoryPath))
			{
				MessageBox.Show("File with this name already exsits");
				return;
			}
			else
			{
				Directory.CreateDirectory(directoryPath);
			}
		}

		private void UpdateTasks()
		{
			for (int i = 0; i < taskList.Count(); i++)
			{
				if (gridView.Rows[i].Cells[1].Value != null)
				{
					taskList.GetList()[i].Name = gridView.Rows[i].Cells[1].Value.ToString();
				}
				if (gridView.Rows[i].Cells[0].Value != null)
				{
					taskList.GetList()[i].Id = (int)gridView.Rows[i].Cells[0].Value;
				}
			}
		}

		//environment
		private void addButton_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(taskList.GetPathToList()) && File.Exists(taskList.GetPathToList()))
			{
				int newId = taskList.Count() != 0 ? taskList.GetList().Max(t => t.Id) + 1 : 1;

				Task task = new Task(newId, "", false);
				taskList.AddElement(task);

				UpdateGridView();

				deleteButton.Enabled = taskList.Count() > 0;
				infoTextBox.Text = calculatePercentageByList(taskList).ToString();

				AnimateButton(addButton, Color.Green, 35);
			}
		}

		private void createToolStripMenuItem_Click(object sender, EventArgs e)//createToolStrip
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

							listName = form.enteredName.Trim();
							directoryPath = path + listName + "\\";

							fileNameList = directoryPath + listName + ".json";
							fileNameInfo = directoryPath + listName + "_Info.json";

							taskList.setPathToList(fileNameList);
							taskList.setPathToInfo(fileNameInfo);


							if (listName == "L")
							{
								form.Close();
								break;
							}
							else if (!string.IsNullOrEmpty(listName) && !File.Exists(fileNameList))
							{
								addButton.Enabled = true;
								deleteButton.Enabled = true;
								SaveButton.Enabled = true;

								taskList.Clear();
								UpdateGridView();

								CreateDirectory(directoryPath);
								File.Create(fileNameList).Close();
								File.Create(fileNameInfo).Close();
								this.Text = listName;
								listInfo.dateTime = DateTime.Today;

								break;
							}
							else
							{
								MessageBox.Show("Invalid name");
							}
						}
						else
						{
							MessageBox.Show("Something went wrong...");
						}

					}
				}

			}
		}

		private void gridView_SelectionChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (gridView.SelectedRows.Count > 0)
			{
				int index = gridView.SelectedRows[0].Index;
				if (index >= 0 && index < taskList.Count())
				{
					deleteButton.Enabled = true;
				}
			}
			else
			{
				deleteButton.Enabled = false;
			}
		}

		private void deleteButton_Click(object sender, EventArgs e)
		{
			if (taskList.Count() > 0)
			{
				taskList.GetList().RemoveAt(gridView.RowCount - 1);
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

		private void SaveButton_Click(object sender, EventArgs e)
		{
			UpdateTasks();
			string jsonList = JsonConvert.SerializeObject(taskList.GetList(), Formatting.Indented);
			string jsonInfo = JsonConvert.SerializeObject(listInfo, Formatting.Indented);

			var latestDir = new DirectoryInfo(path)
				.GetDirectories()
				.OrderByDescending(f => f.LastAccessTime)
				.FirstOrDefault();

			var latestInfoFile = latestDir?.GetFiles()
					.Where(f => f.Name.Contains("Info")).FirstOrDefault();

			if (!File.Exists(taskList.GetPathToList()))
			{
				//potentially dangerous code
				this.Name = "To-Do List";
				taskList.Clear();

				MessageBox.Show("File is deleted");
				return;
			}
			else
			{
				if (latestInfoFile == null)
					fileNameInfo = openedFilePath;
				else
					fileNameInfo = latestInfoFile.FullName;

				File.WriteAllText(taskList.GetPathToList(), jsonList);
				File.WriteAllText(fileNameInfo, jsonInfo);

				using (var progress = new ProgressDBContext())
				{
					DateTime today = DateTime.Today;
					DateTime tommorow = today.AddDays(1);

					ListInfo existing = progress.progresses.FirstOrDefault(p => p.dateTime >= today && p.dateTime < tommorow);

					if (existing != null)
					{
						existing.DonePercentage = listInfo.DonePercentage;
						existing.GotStar = listInfo.GotStar;
					}
					else
					{
						ListInfo currentProgress = new ListInfo(listInfo.DonePercentage, DateTime.Now);
						progress.progresses.Add(currentProgress);
					}
					progress.SaveChanges();
				}
				using (var user = new UsersDBContext())
				{
					UserInfo existing = new UserInfo();
					existing = user.users.FirstOrDefault(u => u.Id == 1);

					if (existing != null)
					{
						existing.SetStarList(userInfo.stars);
					}
					else
					{
						UserInfo currentProgress = new UserInfo();
						currentProgress.SetStarList(userInfo.stars);
						user.Add(currentProgress);
					}
					user.SaveChanges();
				}

			}
			AnimateButton(SaveButton, Color.ForestGreen, 60);
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = path;
				openFileDialog.Multiselect = false;
				openFileDialog.Filter = "Json Files (*.json)|*.json|All Files (*.*)| *.*";

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					addButton.Enabled = true;
					deleteButton.Enabled = true;
					SaveButton.Enabled = true;

					taskList.setPathToList(openFileDialog.FileName);

					openedFileName = Path.GetFileNameWithoutExtension(taskList.GetPathToList());

					string PathToCurrentDirectory = Path.GetDirectoryName(taskList.GetPathToList());
					Directory.SetLastAccessTime(taskList.GetPathToList(), DateTime.Now);

					taskList.Clear();
					taskList.SetList(readFile<List<Task>>(taskList.GetPathToList()));
					infoTextBox.Text = calculatePercentageByList(taskList).ToString();

					fileNameList = openedFilePath;
					UpdateGridView();
					this.Text = openedFileName;
				}
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (folderBrowserDialog2)
			{
				folderBrowserDialog2.InitialDirectory = path;
				using (approveClosingList form = new approveClosingList())
				{
					if (folderBrowserDialog2.ShowDialog() == DialogResult.OK && form.ShowDialog() == DialogResult.OK)
					{
						UpdateGridView();

						string toDeleteDirectoryPath = folderBrowserDialog2.SelectedPath;
						Directory.Delete(toDeleteDirectoryPath, true);
					}
				}

			}
		}

		private void gridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			int doneColumnIndex = 2;

			if (gridView.CurrentCell != null && gridView.CurrentCell.ColumnIndex == doneColumnIndex)
			{
				// Commit the edit so that CellValueChanged is triggered immediately
				gridView.CommitEdit(DataGridViewDataErrorContexts.Commit);


				listInfo.DonePercentage = calculatePercentageByList(taskList);
				infoTextBox.Text = listInfo.DonePercentage.ToString();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			using (var progress = new ProgressDBContext())
			{
				foreach (var p in progress.progresses)
				{
					MessageBox.Show(p.ToString() + $"\n {p.Id}\n{p.DonePercentage}\n{p.dateTime.Date.ToString("dd/MM/yyyy")}");
				}
			}
		}

		private void listInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var progress = new ProgressDBContext())
			{
				using (var transaction = progress.Database.BeginTransaction())
				{
					try
					{
						// Remove all records from the progresses table
						progress.progresses.RemoveRange(progress.progresses);
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
				user.users.RemoveRange(user.users);
				user.SaveChanges();
			}
		}
	}
}