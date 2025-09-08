namespace ToDoList_C_
{
	partial class TrainingUserControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			addButton = new Button();
			deleteButton = new Button();
			SaveButton = new Button();
			flowLayoutPanel1 = new FlowLayoutPanel();
			SuspendLayout();
			// 
			// addButton
			// 
			addButton.Font = new Font("Verdana", 13.8F, FontStyle.Bold);
			addButton.ForeColor = Color.Green;
			addButton.Location = new Point(3, 2);
			addButton.Margin = new Padding(3, 2, 3, 2);
			addButton.Name = "addButton";
			addButton.Size = new Size(109, 45);
			addButton.TabIndex = 7;
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
			deleteButton.Location = new Point(117, 2);
			deleteButton.Margin = new Padding(3, 2, 3, 2);
			deleteButton.Name = "deleteButton";
			deleteButton.Size = new Size(109, 45);
			deleteButton.TabIndex = 8;
			deleteButton.Text = "DELETE";
			deleteButton.UseVisualStyleBackColor = false;
			deleteButton.Click += deleteButton_Click;
			// 
			// SaveButton
			// 
			SaveButton.BackColor = Color.FromArgb(128, 255, 128);
			SaveButton.Font = new Font("Verdana", 13.8F, FontStyle.Bold);
			SaveButton.ForeColor = Color.Black;
			SaveButton.Location = new Point(589, 2);
			SaveButton.Margin = new Padding(3, 2, 3, 2);
			SaveButton.Name = "SaveButton";
			SaveButton.Size = new Size(116, 45);
			SaveButton.TabIndex = 9;
			SaveButton.Text = "Save";
			SaveButton.UseVisualStyleBackColor = false;
			SaveButton.Click += SaveButton_Click_1;
			// 
			// flowLayoutPanel1
			// 
			flowLayoutPanel1.AutoScroll = true;
			flowLayoutPanel1.Location = new Point(3, 52);
			flowLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new Size(703, 273);
			flowLayoutPanel1.TabIndex = 10;
			// 
			// TrainingUserControl
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(flowLayoutPanel1);
			Controls.Add(SaveButton);
			Controls.Add(addButton);
			Controls.Add(deleteButton);
			Margin = new Padding(3, 2, 3, 2);
			Name = "TrainingUserControl";
			Size = new Size(708, 327);
			ResumeLayout(false);
		}

		#endregion

		private Button addButton;
		private Button deleteButton;
		private Button SaveButton;
		private FlowLayoutPanel flowLayoutPanel1;
	}
}
