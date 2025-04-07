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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
			showingFireWV2 = new Microsoft.Web.WebView2.WinForms.WebView2();
			menuStrip1 = new MenuStrip();
			fileToolStripMenuItem = new ToolStripMenuItem();
			createToolStripItem = new ToolStripMenuItem();
			openToolStripMenuItem = new ToolStripMenuItem();
			closeToolStripMenuItem1 = new ToolStripMenuItem();
			deleteToolStripMenuItem = new ToolStripMenuItem();
			gridView = new DataGridView();
			addButton = new Button();
			deleteButton = new Button();
			folderBrowserDialog1 = new FolderBrowserDialog();
			SaveButton = new Button();
			infoTextBox = new TextBox();
			textbox2 = new TextBox();
			showingStarsWV2 = new Microsoft.Web.WebView2.WinForms.WebView2();
			folderBrowserDialog2 = new FolderBrowserDialog();
			((System.ComponentModel.ISupportInitialize)showingFireWV2).BeginInit();
			menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)gridView).BeginInit();
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
			showingFireWV2.Location = new Point(420, 36);
			showingFireWV2.Name = "showingFireWV2";
			showingFireWV2.Size = new Size(66, 29);
			showingFireWV2.TabIndex = 11;
			showingFireWV2.ZoomFactor = 1D;
			// 
			// menuStrip1
			// 
			menuStrip1.ImageScalingSize = new Size(20, 20);
			menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
			menuStrip1.Location = new Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new Size(800, 28);
			menuStrip1.TabIndex = 3;
			menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { createToolStripItem, openToolStripMenuItem, closeToolStripMenuItem1, deleteToolStripMenuItem });
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
			// closeToolStripMenuItem1
			// 
			closeToolStripMenuItem1.Name = "closeToolStripMenuItem1";
			closeToolStripMenuItem1.Size = new Size(136, 26);
			closeToolStripMenuItem1.Text = "Close";
			closeToolStripMenuItem1.Click += closeToolStripMenuItem1_Click;
			// 
			// deleteToolStripMenuItem
			// 
			deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			deleteToolStripMenuItem.Size = new Size(136, 26);
			deleteToolStripMenuItem.Text = "Delete";
			deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
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
			gridView.Location = new Point(12, 91);
			gridView.Name = "gridView";
			gridView.RowHeadersVisible = false;
			gridView.RowHeadersWidth = 51;
			gridView.Size = new Size(776, 347);
			gridView.TabIndex = 4;
			// 
			// addButton
			// 
			addButton.Font = new Font("Verdana", 13.8F, FontStyle.Bold);
			addButton.ForeColor = Color.Green;
			addButton.Location = new Point(12, 32);
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
			deleteButton.Location = new Point(143, 32);
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
			SaveButton.Location = new Point(655, 32);
			SaveButton.Name = "SaveButton";
			SaveButton.Size = new Size(133, 53);
			SaveButton.TabIndex = 7;
			SaveButton.Text = "Save";
			SaveButton.UseVisualStyleBackColor = false;
			SaveButton.Click += SaveButton_Click;
			// 
			// infoTextBox
			// 
			infoTextBox.BackColor = SystemColors.Menu;
			infoTextBox.BorderStyle = BorderStyle.None;
			infoTextBox.Font = new Font("Segoe UI Emoji", 9F);
			infoTextBox.Location = new Point(405, 45);
			infoTextBox.Name = "infoTextBox";
			infoTextBox.Size = new Size(125, 20);
			infoTextBox.TabIndex = 8;
			infoTextBox.Text = "null";
			// 
			// textbox2
			// 
			textbox2.BackColor = SystemColors.Menu;
			textbox2.BorderStyle = BorderStyle.None;
			textbox2.Font = new Font("Segoe UI Emoji", 9F);
			textbox2.Location = new Point(274, 45);
			textbox2.Name = "textbox2";
			textbox2.Size = new Size(125, 20);
			textbox2.TabIndex = 9;
			textbox2.Text = "Tasks Done Today:";
			// 
			// showingStarsWV2
			// 
			showingStarsWV2.AllowExternalDrop = true;
			showingStarsWV2.BackColor = SystemColors.ButtonFace;
			showingStarsWV2.BackgroundImageLayout = ImageLayout.Center;
			showingStarsWV2.CreationProperties = null;
			showingStarsWV2.DefaultBackgroundColor = Color.White;
			showingStarsWV2.Location = new Point(492, 36);
			showingStarsWV2.Name = "showingStarsWV2";
			showingStarsWV2.Size = new Size(157, 49);
			showingStarsWV2.TabIndex = 12;
			showingStarsWV2.ZoomFactor = 1D;
			// 
			// mainForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(showingStarsWV2);
			Controls.Add(showingFireWV2);
			Controls.Add(textbox2);
			Controls.Add(infoTextBox);
			Controls.Add(SaveButton);
			Controls.Add(deleteButton);
			Controls.Add(addButton);
			Controls.Add(gridView);
			Controls.Add(menuStrip1);
			Font = new Font("Segoe UI Emoji", 9F);
			Icon = (Icon)resources.GetObject("$this.Icon");
			MainMenuStrip = menuStrip1;
			Name = "mainForm";
			Text = "To Do List";
			Load += mainForm_Load;
			((System.ComponentModel.ISupportInitialize)showingFireWV2).EndInit();
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)gridView).EndInit();
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
		private Microsoft.Web.WebView2.WinForms.WebView2 showingStarsWV2;
		private FolderBrowserDialog folderBrowserDialog2;
	}
}
