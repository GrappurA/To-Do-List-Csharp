using Microsoft.IdentityModel.Tokens;
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
	public partial class GetListNameForm : Form
	{
		public GetListNameForm()
		{
			InitializeComponent();
			this.FormClosed += GetListNameForm_FormClosed;
		}

		public string enteredName { get; private set; }

		private void okButton_Click(object sender, EventArgs e)
		{
			enteredName = getNameTextBox.Text;
			this.DialogResult = DialogResult.OK;
			this.Close();

		}
		private void GetListNameForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (enteredName.IsNullOrEmpty())
			{
				this.DialogResult = DialogResult.Cancel;
			}
			else
			{
				this.DialogResult = DialogResult.OK;
			}
		}
		
	}
}
