namespace ToDoList_C_
{
	partial class openListForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(openListForm));
			showTaskListsDGV = new DataGridView();
			textBox1 = new TextBox();
			((System.ComponentModel.ISupportInitialize)showTaskListsDGV).BeginInit();
			SuspendLayout();
			// 
			// showTaskListsDGV
			// 
			showTaskListsDGV.BackgroundColor = SystemColors.Control;
			showTaskListsDGV.BorderStyle = BorderStyle.None;
			showTaskListsDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			showTaskListsDGV.Location = new Point(0, 0);
			showTaskListsDGV.Name = "showTaskListsDGV";
			showTaskListsDGV.RowHeadersWidth = 51;
			showTaskListsDGV.Size = new Size(673, 188);
			showTaskListsDGV.TabIndex = 0;
			showTaskListsDGV.CellClick += showTaskListsDGV_CellClick;
			// 
			// textBox1
			// 
			textBox1.BackColor = SystemColors.Control;
			textBox1.BorderStyle = BorderStyle.None;
			textBox1.Font = new Font("Verdana", 12.8F, FontStyle.Bold);
			textBox1.Location = new Point(709, 71);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(179, 47);
			textBox1.TabIndex = 2;
			textBox1.Text = "Press 'Space'\r\nTo submit";
			textBox1.TextAlign = HorizontalAlignment.Center;
			// 
			// openListForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.Control;
			ClientSize = new Size(931, 189);
			Controls.Add(textBox1);
			Controls.Add(showTaskListsDGV);
			Icon = (Icon)resources.GetObject("$this.Icon");
			KeyPreview = true;
			MaximizeBox = false;
			Name = "openListForm";
			Text = "Open a List";
			KeyDown += openListForm_KeyDown;
			((System.ComponentModel.ISupportInitialize)showTaskListsDGV).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private DataGridView showTaskListsDGV;
		private Button button1;
		private TextBox textBox1;
	}
}