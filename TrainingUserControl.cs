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

		private void GetInfoFromTextBoxes()
		{
			

		}

		private async void SaveButton_Click(object sender, EventArgs e)
		{
			GetInfoFromTextBoxes();

			string json = JsonConvert.SerializeObject(info, Formatting.Indented);
			await File.WriteAllTextAsync(filePath, json);
			AnimateButton(SaveButton, Color.Green, 35);
		}

		private void LoadLatestData()
		{
			string json = File.ReadAllText(filePath);
			info = JsonConvert.DeserializeObject<string[]>(json).ToList();			
		}

		async void AnimateButton(Button button, Color color, int delay)
		{
			Color originalColor = button.BackColor;
			button.BackColor = color;
			await System.Threading.Tasks.Task.Delay(delay);
			button.BackColor = originalColor;
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			var panel = new labeledTextBox();
			flowLayoutPanel1.Controls.Add(panel);
			
		}

		private void deleteButton_Click(object sender, EventArgs e)
		{

		}		
	}

}
