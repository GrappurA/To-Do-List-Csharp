using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_C_
{
	internal class ListInfo
	{
		public int Id { get; set; }

		public int DonePercentage { get; set; }

		public DateTime dateTime { get; set; }

		public bool GotStar { get; set; }


		public ListInfo() { }

		public ListInfo(TaskList taskList)
		{
			this.GotStar = taskList.GetGotStarStatus();
		}

		public ListInfo(int donePercentage, DateTime dt)
		{
			this.DonePercentage = donePercentage;
			this.dateTime = dt;
		}

	}
}
