using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_C_
{
	internal class TaskList
	{
		bool gotStar;
		List<Task> taskList;
		
		public TaskList()
		{
			taskList = new List<Task>();
			gotStar = false;
		}

		public void SetGotStarStatus(bool gotStar)
		{
			this.gotStar = gotStar;
		}

		public void AddElement(Task elem)
		{
			taskList.Add(elem);
		}

		public void RemoveElement(Task elem)
		{
			taskList.Remove(elem);
		}

	}
}
