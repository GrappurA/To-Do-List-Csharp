namespace ToDoList_C_
{
	partial class mainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
			showingFireWV2 = new Microsoft.Web.WebView2.WinForms.WebView2();
			menuStrip1 = new MenuStrip();
			fileToolStripMenuItem = new ToolStripMenuItem();
			createToolStripItem = new ToolStripMenuItem();
			openToolStripMenuItem = new ToolStripMenuItem();
			deleteToolStripMenuItem = new ToolStripMenuItem();
			closeToolStripMenuItem1 = new ToolStripMenuItem();
			trainingToolStripMenuItem = new ToolStripMenuItem();
			createToolStripMenuItem = new ToolStripMenuItem();
			gridView = new DataGridView();
			addButton = new Button();
			deleteButton = new Button();
			folderBrowserDialog1 = new FolderBrowserDialog();
			SaveButton = new Button();
			infoTextBox = new TextBox();
			textbox2 = new TextBox();
			folderBrowserDialog2 = new FolderBrowserDialog();
			mainTabControl = new TabControl();
			tabPage2 = new TabPage();
			tabPage1 = new TabPage();
			textBox1 = new TextBox();
			chooseLastDaysCB = new ComboBox();
			percentageToDaysChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			fireFrameGifBP = new PictureBox();
			showMaxStreakWV = new Microsoft.Web.WebView2.WinForms.WebView2();
			showDayStreak = new Microsoft.Web.WebView2.WinForms.WebView2();
			showLatestStarWV = new Microsoft.Web.WebView2.WinForms.WebView2();
			showingStarsWV2 = new Microsoft.Web.WebView2.WinForms.WebView2();
			((System.ComponentModel.ISupportInitialize)showingFireWV2).BeginInit();
			menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)gridView).BeginInit();
			mainTabControl.SuspendLayout();
			tabPage2.SuspendLayout();
			tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)percentageToDaysChart).BeginInit();
			((System.ComponentModel.ISupportInitialize)fireFrameGifBP).BeginInit();
			((System.ComponentModel.ISupportInitialize)showMaxStreakWV).BeginInit();
			((System.ComponentModel.ISupportInitialize)showDayStreak).BeginInit();
			((System.ComponentModel.ISupportInitialize)showLatestStarWV).BeginInit();
			((System.ComponentModel.ISupportInitialize)showingStarsWV2).BeginInit();
			SuspendLayout();
			// 
			// showingFireWV2
			// 
			showingFireWV2.AllowExternalDrop = false;
			showingFireWV2.BackgroundImageLayout = ImageLayout.None;
			showingFireWV2.CreationProperties = null;
			showingFireWV2.DefaultBackgroundColor = Color.Transparent;
			showingFireWV2.Enabled = false;
			showingFireWV2.Location = new Point(414, 10);
			showingFireWV2.Name = "showingFireWV2";
			showingFireWV2.Size = new Size(66, 29);
			showingFireWV2.TabIndex = 11;
			showingFireWV2.ZoomFactor = 1D;
			// 
			// menuStrip1
			// 
			menuStrip1.BackColor = SystemColors.ButtonFace;
			menuStrip1.ImageScalingSize = new Size(20, 20);
			menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, trainingToolStripMenuItem });
			menuStrip1.Location = new Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new Size(818, 28);
			menuStrip1.TabIndex = 3;
			menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { createToolStripItem, openToolStripMenuItem, deleteToolStripMenuItem, closeToolStripMenuItem1 });
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new Size(46, 24);
			fileToolStripMenuItem.Text = "File";
			// 
			// createToolStripItem
			// 
			createToolStripItem.Name = "createToolStripItem";
			createToolStripItem.Size = new Size(136, 26);
			createToolStripItem.Text = "Create";
			createToolStripItem.Click += createToolStripMenuItem_Click;
			// 
			// openToolStripMenuItem
			// 
			openToolStripMenuItem.Name = "openToolStripMenuItem";
			openToolStripMenuItem.Size = new Size(136, 26);
			openToolStripMenuItem.Text = "Open";
			openToolStripMenuItem.Click += openToolStripMenuItem_Click;
			// 
			// deleteToolStripMenuItem
			// 
			deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			deleteToolStripMenuItem.Size = new Size(136, 26);
			deleteToolStripMenuItem.Text = "Delete";
			deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
			// 
			// closeToolStripMenuItem1
			// 
			closeToolStripMenuItem1.Name = "closeToolStripMenuItem1";
			closeToolStripMenuItem1.Size = new Size(136, 26);
			closeToolStripMenuItem1.Text = "Close";
			closeToolStripMenuItem1.Click += closeToolStripMenuItem1_Click;
			// 
			// trainingToolStripMenuItem
			// 
			trainingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { createToolStripMenuItem });
			trainingToolStripMenuItem.Name = "trainingToolStripMenuItem";
			trainingToolStripMenuItem.Size = new Size(76, 24);
			trainingToolStripMenuItem.Text = "Training";
			// 
			// createToolStripMenuItem
			// 
			createToolStripMenuItem.Name = "createToolStripMenuItem";
			createToolStripMenuItem.Size = new Size(135, 26);
			createToolStripMenuItem.Text = "Create";
			// 
			// gridView
			// 
			dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
			gridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			gridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
			gridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("Segoe UI Emoji", 9F);
			dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
			gridView.DefaultCellStyle = dataGridViewCellStyle2;
			gridView.Location = new Point(0, 65);
			gridView.Name = "gridView";
			gridView.RowHeadersVisible = false;
			gridView.RowHeadersWidth = 51;
			gridView.Size = new Size(806, 375);
			gridView.TabIndex = 4;
			// 
			// addButton
			// 
			addButton.Font = new Font("Verdana", 13.8F, FontStyle.Bold);
			addButton.ForeColor = Color.Green;
			addButton.Location = new Point(0, 6);
			addButton.Name = "addButton";
			addButton.Size = new Size(125, 53);
			addButton.TabIndex = 6;
			addButton.Text = "ADD";
			addButton.UseVisualStyleBackColor = true;
			addButton.Click += addButton_Click;
			// 
			// deleteButton
			// 
			deleteButton.BackColor = Color.Transparent;
			deleteButton.Enabled = false;
			deleteButton.Font = new Font("Verdana", 13.8F, FontStyle.Bold);
			deleteButton.ForeColor = Color.Red;
			deleteButton.Location = new Point(131, 6);
			deleteButton.Name = "deleteButton";
			deleteButton.Size = new Size(125, 53);
			deleteButton.TabIndex = 6;
			deleteButton.Text = "DELETE";
			deleteButton.UseVisualStyleBackColor = false;
			deleteButton.Click += deleteButton_Click;
			// 
			// folderBrowserDialog1
			// 
			folderBrowserDialog1.InitialDirectory = "G:\\Main\\ToDoLists";
			// 
			// SaveButton
			// 
			SaveButton.BackColor = Color.FromArgb(128, 255, 128);
			SaveButton.Font = new Font("Verdana", 13.8F, FontStyle.Bold);
			SaveButton.ForeColor = Color.Black;
			SaveButton.Location = new Point(673, 10);
			SaveButton.Name = "SaveButton";
			SaveButton.Size = new Size(133, 53);
			SaveButton.TabIndex = 7;
			SaveButton.Text = "Save";
			SaveButton.UseVisualStyleBackColor = false;
			SaveButton.Click += SaveButton_Click;
			// 
			// infoTextBox
			// 
			infoTextBox.BackColor = SystemColors.ControlLightLight;
			infoTextBox.BorderStyle = BorderStyle.None;
			infoTextBox.Font = new Font("Segoe UI Emoji", 9F);
			infoTextBox.Location = new Point(399, 19);
			infoTextBox.Name = "infoTextBox";
			infoTextBox.Size = new Size(125, 20);
			infoTextBox.TabIndex = 8;
			infoTextBox.Text = "null";
			// 
			// textbox2
			// 
			textbox2.BackColor = SystemColors.ControlLightLight;
			textbox2.BorderStyle = BorderStyle.None;
			textbox2.Font = new Font("Segoe UI Emoji", 9F);
			textbox2.Location = new Point(268, 19);
			textbox2.Name = "textbox2";
			textbox2.Size = new Size(125, 20);
			textbox2.TabIndex = 9;
			textbox2.Text = "Tasks Done Today:";
			// 
			// mainTabControl
			// 
			mainTabControl.Controls.Add(tabPage2);
			mainTabControl.Controls.Add(tabPage1);
			mainTabControl.Location = new Point(0, 28);
			mainTabControl.Name = "mainTabControl";
			mainTabControl.SelectedIndex = 0;
			mainTabControl.Size = new Size(817, 469);
			mainTabControl.SizeMode = TabSizeMode.FillToRight;
			mainTabControl.TabIndex = 14;
			// 
			// tabPage2
			// 
			tabPage2.Controls.Add(addButton);
			tabPage2.Controls.Add(gridView);
			tabPage2.Controls.Add(deleteButton);
			tabPage2.Controls.Add(showingFireWV2);
			tabPage2.Controls.Add(SaveButton);
			tabPage2.Controls.Add(textbox2);
			tabPage2.Controls.Add(infoTextBox);
			tabPage2.Location = new Point(4, 29);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new Padding(3);
			tabPage2.Size = new Size(809, 436);
			tabPage2.TabIndex = 1;
			tabPage2.Text = "List";
			tabPage2.UseVisualStyleBackColor = true;
			// 
			// tabPage1
			// 
			tabPage1.Controls.Add(textBox1);
			tabPage1.Controls.Add(chooseLastDaysCB);
			tabPage1.Controls.Add(percentageToDaysChart);
			tabPage1.Controls.Add(fireFrameGifBP);
			tabPage1.Controls.Add(showMaxStreakWV);
			tabPage1.Controls.Add(showDayStreak);
			tabPage1.Controls.Add(showLatestStarWV);
			tabPage1.Controls.Add(showingStarsWV2);
			tabPage1.Location = new Point(4, 29);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new Padding(3);
			tabPage1.Size = new Size(809, 436);
			tabPage1.TabIndex = 2;
			tabPage1.Text = "Statistics";
			tabPage1.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			textBox1.BackColor = Color.White;
			textBox1.BorderStyle = BorderStyle.None;
			textBox1.Font = new Font("Verdana", 12.8F, FontStyle.Bold);
			textBox1.Location = new Point(556, 232);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.ReadOnly = true;
			textBox1.Size = new Size(211, 55);
			textBox1.TabIndex = 19;
			textBox1.Text = "Change Graph ^";
			textBox1.TextAlign = HorizontalAlignment.Center;
			// 
			// chooseLastDaysCB
			// 
			chooseLastDaysCB.BackColor = Color.Salmon;
			chooseLastDaysCB.DropDownStyle = ComboBoxStyle.DropDownList;
			chooseLastDaysCB.FlatStyle = FlatStyle.Popup;
			chooseLastDaysCB.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
			chooseLastDaysCB.FormattingEnabled = true;
			chooseLastDaysCB.Location = new Point(613, 198);
			chooseLastDaysCB.Name = "chooseLastDaysCB";
			chooseLastDaysCB.RightToLeft = RightToLeft.No;
			chooseLastDaysCB.Size = new Size(147, 28);
			chooseLastDaysCB.TabIndex = 18;
			// 
			// percentageToDaysChart
			// 
			percentageToDaysChart.BorderlineColor = Color.Black;
			chartArea1.Name = "ChartArea1";
			percentageToDaysChart.ChartAreas.Add(chartArea1);
			legend1.Name = "Legend1";
			percentageToDaysChart.Legends.Add(legend1);
			percentageToDaysChart.Location = new Point(8, 198);
			percentageToDaysChart.Name = "percentageToDaysChart";
			percentageToDaysChart.PaletteCustomColors = new Color[]
	{
	Color.FromArgb(0, 0, 0, 44)
	};
			series1.ChartArea = "ChartArea1";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
			series1.Font = new Font("Microsoft YaHei UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			series1.IsValueShownAsLabel = true;
			series1.Legend = "Legend1";
			series1.MarkerColor = Color.Red;
			series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
			series1.Name = "Series1";
			series1.YValuesPerPoint = 10;
			percentageToDaysChart.Series.Add(series1);
			percentageToDaysChart.Size = new Size(599, 231);
			percentageToDaysChart.TabIndex = 17;
			// 
			// fireFrameGifBP
			// 
			fireFrameGifBP.Image = (Image)resources.GetObject("fireFrameGifBP.Image");
			fireFrameGifBP.Location = new Point(5, 10);
			fireFrameGifBP.Name = "fireFrameGifBP";
			fireFrameGifBP.Size = new Size(278, 64);
			fireFrameGifBP.SizeMode = PictureBoxSizeMode.StretchImage;
			fireFrameGifBP.TabIndex = 16;
			fireFrameGifBP.TabStop = false;
			// 
			// showMaxStreakWV
			// 
			showMaxStreakWV.AllowExternalDrop = true;
			showMaxStreakWV.CreationProperties = null;
			showMaxStreakWV.DefaultBackgroundColor = Color.White;
			showMaxStreakWV.Location = new Point(0, 139);
			showMaxStreakWV.Name = "showMaxStreakWV";
			showMaxStreakWV.Size = new Size(335, 53);
			showMaxStreakWV.TabIndex = 15;
			showMaxStreakWV.ZoomFactor = 1D;
			// 
			// showDayStreak
			// 
			showDayStreak.AllowExternalDrop = true;
			showDayStreak.CreationProperties = null;
			showDayStreak.DefaultBackgroundColor = Color.White;
			showDayStreak.Location = new Point(5, 80);
			showDayStreak.Name = "showDayStreak";
			showDayStreak.Size = new Size(374, 53);
			showDayStreak.TabIndex = 15;
			showDayStreak.ZoomFactor = 1D;
			// 
			// showLatestStarWV
			// 
			showLatestStarWV.AllowExternalDrop = true;
			showLatestStarWV.CreationProperties = null;
			showLatestStarWV.DefaultBackgroundColor = Color.White;
			showLatestStarWV.Location = new Point(289, 15);
			showLatestStarWV.Name = "showLatestStarWV";
			showLatestStarWV.Size = new Size(339, 53);
			showLatestStarWV.TabIndex = 14;
			showLatestStarWV.ZoomFactor = 1D;
			// 
			// showingStarsWV2
			// 
			showingStarsWV2.AllowExternalDrop = true;
			showingStarsWV2.BackColor = SystemColors.Control;
			showingStarsWV2.BackgroundImageLayout = ImageLayout.Center;
			showingStarsWV2.CreationProperties = null;
			showingStarsWV2.DefaultBackgroundColor = Color.White;
			showingStarsWV2.Location = new Point(25, 15);
			showingStarsWV2.Name = "showingStarsWV2";
			showingStarsWV2.Size = new Size(253, 46);
			showingStarsWV2.TabIndex = 13;
			showingStarsWV2.ZoomFactor = 1D;
			// 
			// mainForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.ButtonFace;
			ClientSize = new Size(818, 498);
			Controls.Add(mainTabControl);
			Controls.Add(menuStrip1);
			Font = new Font("Segoe UI Emoji", 9F);
			FormBorderStyle = FormBorderStyle.Fixed3D;
			Icon = (Icon)resources.GetObject("$this.Icon");
			MainMenuStrip = menuStrip1;
			MaximizeBox = false;
			Name = "mainForm";
			RightToLeft = RightToLeft.No;
			Text = "To Do List";
			((System.ComponentModel.ISupportInitialize)showingFireWV2).EndInit();
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)gridView).EndInit();
			mainTabControl.ResumeLayout(false);
			tabPage2.ResumeLayout(false);
			tabPage2.PerformLayout();
			tabPage1.ResumeLayout(false);
			tabPage1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)percentageToDaysChart).EndInit();
			((System.ComponentModel.ISupportInitialize)fireFrameGifBP).EndInit();
			((System.ComponentModel.ISupportInitialize)showMaxStreakWV).EndInit();
			((System.ComponentModel.ISupportInitialize)showDayStreak).EndInit();
			((System.ComponentModel.ISupportInitialize)showLatestStarWV).EndInit();
			((System.ComponentModel.ISupportInitialize)showingStarsWV2).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private MenuStrip menuStrip1;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem createToolStripItem;
		private ToolStripMenuItem closeToolStripMenuItem1;
		private DataGridView gridView;
		private Button addButton;
		private Button deleteButton;
		private ToolStripMenuItem openToolStripMenuItem;
		private FolderBrowserDialog folderBrowserDialog1;
		private Button SaveButton;
		private ToolStripMenuItem deleteToolStripMenuItem;
		private TextBox infoTextBox;
		private TextBox textbox2;
		private Microsoft.Web.WebView2.WinForms.WebView2 showingFireWV2;
		private FolderBrowserDialog folderBrowserDialog2;
		private TabControl mainTabControl;
		private TabPage tabPage2;
		private TabPage tabPage1;
		private Microsoft.Web.WebView2.WinForms.WebView2 showingStarsWV2;
		private Microsoft.Web.WebView2.WinForms.WebView2 showLatestStarWV;
		private Microsoft.Web.WebView2.WinForms.WebView2 showDayStreak;
		private Microsoft.Web.WebView2.WinForms.WebView2 showMaxStreakWV;
		private PictureBox fireFrameGifBP;
		private System.Windows.Forms.DataVisualization.Charting.Chart percentageToDaysChart;
		private ComboBox chooseLastDaysCB;
		private TextBox textBox1;
		private ToolStripMenuItem trainingToolStripMenuItem;
		private ToolStripMenuItem createToolStripMenuItem;
	}
}
