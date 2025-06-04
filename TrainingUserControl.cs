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
			LoadLatestData();
		}



		static string appFolder = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
			"ToDoList");

		List<string> info = new();
		string filePath = appFolder + "\\trainings.json";

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

		private void GetInfoFromTextBoxes()
		{
			info.Clear();
			info.Add(richTextBox1.Text);
			info.Add(richTextBox2.Text);
			info.Add(richTextBox3.Text);
			info.Add(richTextBox4.Text);
			info.Add(richTextBox5.Text);
		}

		private async void SaveButton_Click(object sender, EventArgs e)
		{
			GetInfoFromTextBoxes();

			string json = JsonConvert.SerializeObject(info, Formatting.Indented);
			await File.WriteAllTextAsync(filePath, json);
			AnimateButton(SaveButton,Color.Green,35);
		}

		private void LoadLatestData()
		{
			string json = File.ReadAllText(filePath);
			info = JsonConvert.DeserializeObject<string[]>(json).ToList();

			richTextBox1.Text= info[0];
			richTextBox2.Text= info[1];
			richTextBox3.Text= info[2];
			richTextBox4.Text= info[3];
			richTextBox5.Text= info[4];
		}

		async void AnimateButton(Button button, Color color, int delay)
		{
			Color originalColor = button.BackColor;
			button.BackColor = color;
			await System.Threading.Tasks.Task.Delay(delay);
			button.BackColor = originalColor;
		}
	}

}
