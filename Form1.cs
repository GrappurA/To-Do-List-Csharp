using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ToDoList_C_
{
	public partial class mainForm : Form
	{
		List<Task> taskList = new List<Task>();
		string path = "G:\\Main\\ToDoLists\\";
		string fileName;
		string listName;


		public mainForm()
		{
			InitializeComponent();

			addButton.Enabled = false;
			deleteButton.Enabled = false;
			SaveButton.Enabled = false;
			gridView.RowHeadersVisible = false;

			openLatestFile();

			folderBrowserDialog1.InitialDirectory = path;
			folderBrowserDialog1.Description = "Open A To Do List file";

			gridView.CurrentCellDirtyStateChanged += gridView_CurrentCellDirtyStateChanged;
			gridView.CellValueChanged += gridView_CellValueChanged;

		}

		//additional funcs
		int calculatePercentageByList(List<Task> taskList)
		{
			int oneTaskPecentage = 100 / taskList.Count;
			int donePercentage = 0;
			for (int i = 0; i < taskList.Count() - 1; i++)
			{
				if (taskList[i].Status == true)
				{
					donePercentage += oneTaskPecentage;
				}
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
				var latestFile = new DirectoryInfo(path).GetFiles().OrderByDescending(f => f.LastAccessTime).FirstOrDefault();
				if (latestFile != null)
				{
					string latestFilePath = latestFile.FullName;
					string formName = latestFile.Name;

					addButton.Enabled = true;
					deleteButton.Enabled = true;
					SaveButton.Enabled = true;
					this.Text = formName;

					taskList.Clear();
					taskList = readFile(latestFilePath);

					fileName = latestFilePath;
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

		private List<Task> readFile(string openedFilePath)
		{
			return JsonConvert.DeserializeObject<List<Task>>(File.ReadAllText(openedFilePath));
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
			if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
			{
				int newId = taskList.Count != 0 ? taskList.Max(t => t.Id) + 1 : 1;

				Task task = new Task(newId, "", false);
				taskList.Add(task);

				UpdateGridView();

				deleteButton.Enabled = taskList.Count > 0;

				// Color originalColor = addButton.BackColor;
				// addButton.BackColor = Color.DarkGreen;
				// await System.Threading.Tasks.Task.Delay(100);
				// addButton.BackColor = originalColor;
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)//createToolStrip
		{
			while (true)
			{
				using (GetListNameForm form = new GetListNameForm())
				{

					if (form.ShowDialog() == DialogResult.OK)
					{
						taskList.Clear();
						UpdateGridView();
						listName = form.enteredName.Trim();
						fileName = path + listName + ".json";
						//latestPath = fileName;
						if (listName == "L")
						{
							form.Close();
							break;
						}
						else if (!string.IsNullOrEmpty(listName) && !File.Exists(fileName))
						{
							addButton.Enabled = true;
							deleteButton.Enabled = true;
							SaveButton.Enabled = true;
							File.Create(fileName).Close();
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
			//if (gridView.CurrentCell == null || taskList.Count == 0) return;

			if (taskList.Count > 0)
			{
				int selectedRowIndex = gridView.CurrentCell.RowIndex;
				if (selectedRowIndex >= 0)
				{
					taskList.RemoveAt(selectedRowIndex);
					UpdateGridView();
				}

			}
			deleteButton.Enabled = taskList.Count > 0;
			SaveButton.Enabled = true;

			//Color originalColor = deleteButton.BackColor;
			//deleteButton.BackColor = Color.Red;
			//await System.Threading.Tasks.Task.Delay(65);
			//deleteButton.BackColor = originalColor;
		}

		private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			SaveButton.Enabled = false;
			addButton.Enabled = false;
			deleteButton.Enabled = false;
			taskList.Clear();
			//latestPath = fileName;
			UpdateGridView();

		}

		private void SaveButton_Click_1(object sender, EventArgs e)
		{
			//Color originalColor = SaveButton.BackColor;

			// SaveButton.BackColor = Color.DarkGreen;
			// await System.Threading.Tasks.Task.Delay(65);
			// SaveButton.BackColor = originalColor;

			UpdateTasks();
			string json = JsonConvert.SerializeObject(taskList, Formatting.Indented);
			File.WriteAllText(fileName, json);


			//latestPath = fileName;
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Multiselect = false;
				openFileDialog.Filter = "Json Files (*.json)|*.json|All Files (*.*)| *.*";

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					addButton.Enabled = true;
					deleteButton.Enabled = true;
					SaveButton.Enabled = true;
					string openedFilePath = openFileDialog.FileName;
					taskList.Clear();
					taskList = readFile(openedFilePath);

					fileName = openedFilePath;
					UpdateGridView();
					//latestPath = fileName;
				}
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog fd = new OpenFileDialog())
			{
				fd.Multiselect = false;
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

		private void gridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex == 2)
			{
				infoTextBox.Text = calculatePercentageByList(taskList).ToString();
			}
		}


	}
}

