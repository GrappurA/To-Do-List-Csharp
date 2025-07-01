using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList_C_
{
	public partial class LabeledTextBox : UserControl
	{
		public string LabelText
		{
			get => textBox1.Text;
			set => textBox1.Text = value;
		}

		public string TextBoxText
		{
			get => richTextBox1.Text;
			set => richTextBox1.Text = value;
		}

		public LabeledTextBox()
		{
			InitializeComponent();
			TextBoxText = richTextBox1.Text;
			LabelText = textBox1.Text;
		}

		public TextBox InnerLabel => textBox1;
		public RichTextBox InnerTextBox => richTextBox1;

		public LabeledTextBoxData GetData()
		{
			return new LabeledTextBoxData()
			{
				LabelData = InnerLabel.Text,
				TextBoxData = InnerTextBox.Text
			};
		}
	}
}
