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

		public List<ToDoTask> taskList { get; set; } = new List<ToDoTask>();

		public int DonePercentage { get; set; }

		public DateTime dateTime { get; set; }

		public bool GotStar { get; set; }

		public string? Name { get; set; }


		public TaskList()
		{
			this.taskList = new List<ToDoTask>();
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

		public void AddElement(ToDoTask elem)
		{
			taskList.Add(elem);
		}

		public void RemoveElement(ToDoTask elem)
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

		public List<ToDoTask> GetList()
		{
			return taskList;
		}

		public void SetList(List<ToDoTask> taskList)
		{
			this.taskList = taskList;
		}

	}
}
