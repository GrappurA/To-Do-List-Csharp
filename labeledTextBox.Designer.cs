namespace ToDoList_C_
{
	partial class LabeledTextBox
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
			richTextBox1 = new RichTextBox();
			textBox1 = new TextBox();
			SuspendLayout();
			// 
			// richTextBox1
			// 
			richTextBox1.BorderStyle = BorderStyle.None;
			richTextBox1.Font = new Font("Verdana", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			richTextBox1.Location = new Point(0, 34);
			richTextBox1.Margin = new Padding(3, 2, 3, 2);
			richTextBox1.Name = "richTextBox1";
			richTextBox1.Size = new Size(218, 107);
			richTextBox1.TabIndex = 0;
			richTextBox1.Text = "";
			// 
			// textBox1
			// 
			textBox1.Font = new Font("Verdana", 13.8F, FontStyle.Bold);
			textBox1.Location = new Point(0, 6);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(218, 30);
			textBox1.TabIndex = 1;
			textBox1.Text = "null";
			textBox1.TextAlign = HorizontalAlignment.Center;
			// 
			// labeledTextBox
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Transparent;
			Controls.Add(textBox1);
			Controls.Add(richTextBox1);
			Margin = new Padding(3, 2, 3, 2);
			Name = "labeledTextBox";
			Size = new Size(219, 140);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private RichTextBox richTextBox1;
		private TextBox textBox1;
	}
}
