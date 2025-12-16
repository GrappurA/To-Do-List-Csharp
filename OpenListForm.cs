using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList_C_
{
	public partial class openListForm : Form
	{
		public openListForm()
		{
			InitializeComponent();
			this.Load += OpenListForm_Load;
			this.KeyDown += openListForm_KeyDown;

		}

		public ListUserWrapper wrapper { get; private set; }

		private void OpenListForm_Load(object? sender, EventArgs e)
		{
			this.Focus();
			wrapper = new();
			textBox1.ReadOnly = true;
			LoadUser();
			LoadList();
			AdjustGridViewLooks();
		}

		private async void LoadUser()
		{
			using (UsersDBContext dbContext = new())
			{
				wrapper.SetUser(await dbContext.users
					.Include(l => l.CurrentList)
					.Include(l => l.stars)
					.FirstOrDefaultAsync());
			}
			if (wrapper.user == null)
			{
				MessageBox.Show("User was null");
				wrapper.SetUser();
			}
		}

		BindingSource _taskList = new BindingSource();
		private async void LoadList()
		{
			using (TaskListDBContext dbContext = new())
			{
				_taskList.DataSource = dbContext.lists
					.Include(l => l.taskList)
					.OrderByDescending(l => l.CreationDate)
					.ToList();
			}
			showTaskListsDGV.DataSource = _taskList;

		}

		private void AdjustGridViewLooks()
		{
			showTaskListsDGV.ReadOnly = true;
			//showTaskListsDGV.ColumnHeadersVisible = false;
			showTaskListsDGV.AllowUserToResizeColumns = false;
			showTaskListsDGV.AllowUserToResizeRows = false;
			showTaskListsDGV.AllowUserToAddRows = false;
			showTaskListsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			showTaskListsDGV.RowHeadersVisible = false;
		}

		private void openListForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space && showTaskListsDGV.SelectedCells != null)
			{
				this.DialogResult = DialogResult.OK;
			}
		}

		int selectedRow;
		private void showTaskListsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				selectedRow = e.RowIndex;
				if (_taskList[selectedRow] is TaskList selectedList)
				{
					wrapper.SetTaskList(selectedList);
				}
			}
		}
	}
}
