using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_C_
{
	internal class ListInfo
	{
		public int DonePercentage { get; set; } 
		public bool GotStar { get; set; }  

		public ListInfo(TaskList taskList)
		{
			this.GotStar = taskList.GetGotStarStatus();
		}

		public ListInfo() { }  
	}
}
