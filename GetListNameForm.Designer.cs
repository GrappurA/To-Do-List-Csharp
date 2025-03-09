namespace ToDoList_C_
{
	partial class GetListNameForm
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
			getNameTextBox = new TextBox();
			okButton = new Button();
			textBox1 = new TextBox();
			textBox2 = new TextBox();
			SuspendLayout();
			// 
			// getNameTextBox
			// 
			getNameTextBox.Font = new Font("Segoe UI", 19.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
			getNameTextBox.Location = new Point(12, 61);
			getNameTextBox.Name = "getNameTextBox";
			getNameTextBox.Size = new Size(188, 50);
			getNameTextBox.TabIndex = 0;
			// 
			// okButton
			// 
			okButton.Location = new Point(206, 61);
			okButton.Name = "okButton";
			okButton.Size = new Size(125, 50);
			okButton.TabIndex = 1;
			okButton.Text = "OK";
			okButton.UseVisualStyleBackColor = true;
			okButton.Click += okButton_Click;
			// 
			// textBox1
			// 
			textBox1.BackColor = SystemColors.Control;
			textBox1.BorderStyle = BorderStyle.None;
			textBox1.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			textBox1.Location = new Point(12, 12);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(203, 31);
			textBox1.TabIndex = 2;
			textBox1.Text = "Creating new list...";
			textBox1.TextAlign = HorizontalAlignment.Center;
			// 
			// textBox2
			// 
			textBox2.BackColor = SystemColors.Control;
			textBox2.BorderStyle = BorderStyle.None;
			textBox2.Font = new Font("Bahnschrift Condensed", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
			textBox2.Location = new Point(-42, 117);
			textBox2.Name = "textBox2";
			textBox2.Size = new Size(292, 21);
			textBox2.TabIndex = 2;
			textBox2.Text = "enter 'L' to close the form";
			textBox2.TextAlign = HorizontalAlignment.Center;
			// 
			// GetListNameForm
			// 
			AcceptButton = okButton;
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(480, 188);
			ControlBox = false;
			Controls.Add(textBox2);
			Controls.Add(textBox1);
			Controls.Add(okButton);
			Controls.Add(getNameTextBox);
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "GetListNameForm";
			Text = "Name List";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox getNameTextBox;
		private Button okButton;
		private TextBox textBox1;
		private TextBox textBox2;
	}
}