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
	public partial class PickDateForm : Form
	{
		public PickDateForm()
		{
			InitializeComponent();
		}
		public DateTime enteredDate {  get; private set; }



	}
}
