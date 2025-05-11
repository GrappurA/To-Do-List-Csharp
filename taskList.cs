using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_C_
{
	public class TaskList
	{
		[Key] public int Id { get; set; }

		public List<Task> taskList { get; set; } = new List<Task>();

		public int DonePercentage { get; set; }

		public DateTime dateTime { get; set; }

		public bool GotStar { get; set; }

		public string? Name { get; set; }


		public TaskList()
		{
			this.taskList = new List<Task>();
			this.DonePercentage = 0;
			this.dateTime = DateTime.Today;
			this.GotStar = false;
			this.Name = "DEFAULT";
		}

		public TaskList(DateTime dt,int donePercentage)
		{			
			this.dateTime= dt;
			this.DonePercentage = donePercentage;
		}

		public void AddElement(Task elem)
		{
			taskList.Add(elem);
		}

		public void RemoveElement(Task elem)
		{
			taskList.Remove(elem);
		}

		public int Count()
		{
			return taskList.Count();
		}

		public void Clear()
		{
			taskList.Clear();
		}

		public List<Task> GetList()
		{
			return taskList;
		}

		public void SetList(List<Task> taskList)
		{
			this.taskList = taskList;
		}

	}
}
