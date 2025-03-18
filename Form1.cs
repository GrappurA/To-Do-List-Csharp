using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ToDoList_C_
{
	public partial class mainForm : Form
	{
		List<Task> taskList = new List<Task>();
		Inventory inventory = new Inventory();


		string path = "G:\\Main\\ToDoLists\\";
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

			textbox1.Font = new Font("Segoe UI Emoji", 9);
			addButton.Enabled = false;
			deleteButton.Enabled = false;
			SaveButton.Enabled = false;
			gridView.RowHeadersVisible = false;
			infoTextBox.ReadOnly = true;
			openLatestFile();

			folderBrowserDialog1.InitialDirectory = path;
			folderBrowserDialog1.Description = "Open A To Do List file";

			gridView.CurrentCellDirtyStateChanged += gridView_CurrentCellDirtyStateChanged;
			calculatePercentageByList(taskList);
		}

		//additional funcs
		async void AnimateButton(Button button, Color color, int delay)
		{
			Color originalColor = button.BackColor;
			button.BackColor = color;
			await System.Threading.Tasks.Task.Delay(delay);
			button.BackColor = originalColor;
		}
		// Color originalColor = addButton.BackColor;
		// addButton.BackColor = Color.DarkGreen;
		// await System.Threading.Tasks.Task.Delay(100);
		// addButton.BackColor = originalColor;

		int calculatePercentageByList(List<Task> taskList)
		{
			if (taskList.Count == 0 || taskList == null) { return 0; }

			int oneTaskPecentage = 100 / taskList.Count;
			int donePercentage = 0;
			int counter = 0;

			for (int i = 0; i < taskList.Count(); i++)
			{
				if (taskList[i].Status == true)
				{
					donePercentage += oneTaskPecentage;
					counter++;
				}
			}
			if (counter == taskList.Count)
			{
				donePercentage = 100;
			}

			//string textBoxOldText = textbox1.Text;
			if (donePercentage > 50 && !textbox1.Text.Contains("🔥"))
			{
				textbox1.Text += "🔥";
			}
			else
			{
				textbox1.Text = "%";
			}

			return donePercentage;
		}

		private void openLatestFile()
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

				//var latestFile
				if (latestListFile != null)
				{
					string latestFilePath = latestListFile.FullName;
					string formName = Path.GetFileNameWithoutExtension(latestFilePath);

					addButton.Enabled = true;
					deleteButton.Enabled = true;
					SaveButton.Enabled = true;
					this.Text = formName;

					taskList.Clear();
					taskList = readListFile(latestFilePath);
					infoTextBox.Text = readJson(latestInfoFile.FullName);

					fileNameList = latestFilePath;


					UpdateGridView();
				}
			}
		}

		private void UpdateGridView()
		{
			if (taskList == null) taskList = new List<Task>();
			gridView.DataSource = null;
			gridView.DataSource = taskList;
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

		private List<Task> readListFile(string filePath)
		{
			return JsonConvert.DeserializeObject<List<Task>>(File.ReadAllText(filePath));
		}

		private string readJson(string filePath)
		{
			return JsonConvert.DeserializeObject<string>(File.ReadAllText(filePath));
		}

		//private int readInfoFile(string )

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
			for (int i = 0; i < taskList.Count; i++)
			{
				if (gridView.Rows[i].Cells[1].Value != null)
				{
					taskList[i].Name = gridView.Rows[i].Cells[1].Value.ToString();
				}
				if (gridView.Rows[i].Cells[0].Value != null)
				{
					taskList[i].Id = (int)gridView.Rows[i].Cells[0].Value;
				}
			}
		}

		//environment
		private void addButton_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(fileNameList) && File.Exists(fileNameList))
			{
				int newId = taskList.Count != 0 ? taskList.Max(t => t.Id) + 1 : 1;

				Task task = new Task(newId, "", false);
				taskList.Add(task);

				UpdateGridView();

				deleteButton.Enabled = taskList.Count > 0;
				infoTextBox.Text = calculatePercentageByList(taskList).ToString();

				AnimateButton(addButton, Color.Green, 60);
			}
		}

		private void createToolStripMenuItem_Click(object sender, EventArgs e)//createToolStrip
		{
			while (true)
			{
				using (GetListNameForm form = new GetListNameForm())
				{

					if (form.ShowDialog() == DialogResult.OK)
					{
						List<Task> taskList = new List<Task>();
						taskList.Clear();
						UpdateGridView();

						listName = form.enteredName.Trim();
						directoryPath = path + listName + "\\";

						fileNameList = directoryPath + listName + ".json";
						fileNameInfo = directoryPath + listName + "_Info.json";

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
							CreateDirectory(directoryPath);
							File.Create(fileNameList).Close();
							File.Create(fileNameInfo).Close();
							this.Text = listName;

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

		private void gridView_SelectionChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (gridView.SelectedRows.Count > 0)
			{
				int index = gridView.SelectedRows[0].Index;
				if (index >= 0 && index < taskList.Count)
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
			if (taskList.Count > 0)
			{
				taskList.RemoveAt(gridView.RowCount - 1);
				UpdateGridView();
			}
			deleteButton.Enabled = taskList.Count > 0;
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
			string jsonList = JsonConvert.SerializeObject(taskList, Formatting.Indented);
			string jsonInfo = JsonConvert.SerializeObject(infoTextBox.Text, Formatting.Indented);

			var latestDir = new DirectoryInfo(path)
				.GetDirectories()
				.OrderByDescending(f => f.LastAccessTime)
				.FirstOrDefault();

			var latestInfoFile = latestDir?.GetFiles()
					.Where(f => f.Name.Contains("Info")).FirstOrDefault();

			if (!File.Exists(fileNameList))
			{
				MessageBox.Show("File is deleted");
				return;
			}
			else
			{
				if (latestInfoFile == null)
					fileNameInfo = openedFilePath;
				else
					fileNameInfo = latestInfoFile.FullName;

				File.WriteAllText(fileNameList, jsonList);
				File.WriteAllText(fileNameInfo, jsonInfo);
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

					openedFilePath = openFileDialog.FileName;
					openedFileName = Path.GetFileNameWithoutExtension(openedFilePath);

					taskList.Clear();
					taskList = readListFile(openedFilePath);
					infoTextBox.Text = calculatePercentageByList(taskList).ToString();

					fileNameList = openedFilePath;
					UpdateGridView();
					this.Text = openedFileName;
				}
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog fd = new OpenFileDialog())
			{
				fd.Multiselect = true;
				fd.Filter = "Json Files (*.json)|*.json";
				using (approveClosingList form = new approveClosingList())
				{
					if (fd.ShowDialog() == DialogResult.OK && form.ShowDialog() == DialogResult.OK)
					{
						taskList.Clear();
						UpdateGridView();

						string toDeletePath = fd.FileName;
						File.Delete(toDeletePath);
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

				infoTextBox.Text = calculatePercentageByList(taskList).ToString();

			}
		}

	}
}