namespace ToDoList_C_
{
	partial class PickDateForm
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
			dateTimePicker1 = new DateTimePicker();
			textBox1 = new TextBox();
			button1 = new Button();
			SuspendLayout();
			// 
			// dateTimePicker1
			// 
			dateTimePicker1.CustomFormat = "d/MM/yyyy";
			dateTimePicker1.Font = new Font("Verdana", 10.2F);
			dateTimePicker1.Location = new Point(12, 40);
			dateTimePicker1.Name = "dateTimePicker1";
			dateTimePicker1.Size = new Size(361, 28);
			dateTimePicker1.TabIndex = 0;
			dateTimePicker1.Value = new DateTime(2025, 4, 10, 0, 0, 0, 0);
			// 
			// textBox1
			// 
			textBox1.BackColor = SystemColors.Control;
			textBox1.BorderStyle = BorderStyle.None;
			textBox1.Font = new Font("Verdana", 10.2F);
			textBox1.Location = new Point(12, 7);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(361, 21);
			textBox1.TabIndex = 1;
			textBox1.Text = "Select the date, that you're planning for: ";
			// 
			// button1
			// 
			button1.BackColor = Color.FromArgb(128, 255, 128);
			button1.Font = new Font("Verdana", 13.8F, FontStyle.Bold);
			button1.Location = new Point(379, 23);
			button1.Name = "button1";
			button1.Size = new Size(113, 45);
			button1.TabIndex = 2;
			button1.Text = "Submit";
			button1.UseVisualStyleBackColor = false;
			button1.Click += button1_Click;
			// 
			// PickDateForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(504, 79);
			Controls.Add(button1);
			Controls.Add(textBox1);
			Controls.Add(dateTimePicker1);
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			Name = "PickDateForm";
			Text = "Pick Date";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private DateTimePicker dateTimePicker1;
		private TextBox textBox1;
		private Button button1;
	}
}