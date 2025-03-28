namespace ToDoList_C_
{
	partial class approveClosingList
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
			OkButton = new Button();
			NoButton = new Button();
			SuspendLayout();
			// 
			// OkButton
			// 
			OkButton.Location = new Point(12, 31);
			OkButton.Name = "OkButton";
			OkButton.Size = new Size(148, 44);
			OkButton.TabIndex = 0;
			OkButton.Text = "YES";
			OkButton.UseVisualStyleBackColor = true;
			OkButton.Click += OkButton_Click_1;
			// 
			// NoButton
			// 
			NoButton.Location = new Point(203, 31);
			NoButton.Name = "NoButton";
			NoButton.Size = new Size(148, 44);
			NoButton.TabIndex = 0;
			NoButton.Text = "NO";
			NoButton.UseVisualStyleBackColor = true;
			NoButton.Click += NoButton_Click;
			// 
			// approveClosingList
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(426, 105);
			Controls.Add(NoButton);
			Controls.Add(OkButton);
			Name = "approveClosingList";
			Text = "Are you sure to close the list?";
			ResumeLayout(false);
		}

		#endregion

		private Button OkButton;
		private Button NoButton;
	}
}