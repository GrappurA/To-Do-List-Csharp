using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace ToDoList_C_
{
	public partial class TrainingUserControl : UserControl
	{
		public TrainingUserControl()
		{
			InitializeComponent();

			Start();
			LoadTrainingData();

			SaveButton.Enabled = false;
			deleteButton.Enabled = false;

		}


		private void AddButtonClicked()
		{
			int count = flowLayoutPanel1.Controls.Count;
			if (count > 0)
			{
				deleteButton.Enabled = true;
				SaveButton.Enabled = true;
			}
		}

		private void DeleteButtonClicked()
		{
			int count = flowLayoutPanel1.Controls.Count;
			if (count < 1)
			{
				deleteButton.Enabled = false;
				SaveButton.Enabled = true;
			}
		}

		static string appFolder = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
			"ToDoList");

		string filePath = appFolder + "\\trainings.json";
		List<string> info = new();

		private void Start()
		{
			if (!Directory.Exists(appFolder))
			{
				Directory.CreateDirectory(appFolder);
			}

			if (!File.Exists(filePath))
			{
				File.Create(filePath);
			}
		}

		static async Task AnimateButton(Button button, Color color, int delay)
		{
			Color originalColor = button.BackColor;
			button.BackColor = color;
			await System.Threading.Tasks.Task.Delay(delay);
			button.BackColor = originalColor;
			await Task.CompletedTask;
		}

		private async void addButton_Click(object sender, EventArgs e)
		{
			await AnimateButton(addButton, color: Color.DarkGreen, 10);
			var panel = new LabeledTextBox();
			flowLayoutPanel1.Controls.Add(panel);

			AddButtonClicked();
		}

		private async void deleteButton_Click(object sender, EventArgs e)
		{
			await AnimateButton(deleteButton, Color.DarkRed, 10);
			Control lastControl = flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1];
			if (lastControl is LabeledTextBox lb)
			{
				flowLayoutPanel1.Controls.Remove(lb);
			}

			DeleteButtonClicked();
		}

		private async void SaveButton_Click_1(object sender, EventArgs e)
		{
			List<LabeledTextBoxData> datas = new List<LabeledTextBoxData>();
			foreach (LabeledTextBox tb in flowLayoutPanel1.Controls)
			{
				datas.Add(tb.GetData());
			}
			string json = JsonConvert.SerializeObject(datas, formatting: Formatting.Indented);
			await File.WriteAllTextAsync(filePath, json);
		}

		private async void LoadTrainingData()
		{
			string json = await File.ReadAllTextAsync(filePath);
			List<LabeledTextBoxData> data = JsonConvert.DeserializeObject<List<LabeledTextBoxData>>(json);

			for (int i = 0; i < data.Count; i++)
			{
				var panel = new LabeledTextBox();
				panel.LabelText = data[i].LabelData;
				panel.TextBoxText = data[i].TextBoxData;
				flowLayoutPanel1.Controls.Add(panel);
			}

			deleteButton.Enabled = data.Count > 0;
			SaveButton.Enabled = data.Count > 0;
		}

	}

}
