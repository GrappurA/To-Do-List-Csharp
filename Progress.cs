using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_C_
{
	public class Progress
	{
		public int Id { get; set; }
		public DateTime dateTime {  get; set; }
		public int DonePercentage { get; set; }

		public Progress(DateTime dateTime, int DonePercentage) 
		{
			this.dateTime = dateTime;
			this.DonePercentage = DonePercentage;
		}
	}
}
