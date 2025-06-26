namespace ToDoList_C_
{
	partial class labeledTextBox
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
			textLabel = new Label();
			SuspendLayout();
			// 
			// richTextBox1
			// 
			richTextBox1.Location = new Point(0, 46);
			richTextBox1.Name = "richTextBox1";
			richTextBox1.Size = new Size(249, 141);
			richTextBox1.TabIndex = 0;
			richTextBox1.Text = "";
			// 
			// textLabel
			// 
			textLabel.Dock = DockStyle.Top;
			textLabel.Font = new Font("Verdana", 13.8F, FontStyle.Bold);
			textLabel.Location = new Point(0, 0);
			textLabel.Name = "textLabel";
			textLabel.Size = new Size(250, 43);
			textLabel.TabIndex = 1;
			textLabel.Text = "text";
			textLabel.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// labeledTextBox
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.Transparent;
			Controls.Add(textLabel);
			Controls.Add(richTextBox1);
			Name = "labeledTextBox";
			Size = new Size(250, 187);
			ResumeLayout(false);
		}

		#endregion

		private RichTextBox richTextBox1;
		private Label textLabel;
	}
}
