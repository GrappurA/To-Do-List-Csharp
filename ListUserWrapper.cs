using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_C_
{
	public partial class ListUserWrapper
	{
		public TaskList taskList { get; private set; }
		public User user { get; private set; }

		public ListUserWrapper()
		{
			taskList = new TaskList();
			user = new User();
		}

		public ListUserWrapper(TaskList ts, User user)
		{
			taskList = ts;
			this.user = user;
		}

		public void SetTaskList(TaskList ts)
		{
			this.taskList = new TaskList(ts);
		}

		public void SetUser(User user)
		{
			this.user = user;
		}

		public void SetUser()
		{
			this.user = new User();
		}
	}
}
