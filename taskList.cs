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

		public DateTime CreationDate { get; set; }

		public DateTime? DueDate { get; set; }

		public bool GotStar { get; set; }

		public string? Name { get; set; }


		public TaskList()
		{
			this.taskList = new List<ToDoTask>();
			this.DonePercentage = 0;
			this.CreationDate = DateTime.Today;
			this.GotStar = false;
			this.Name = "DEFAULT";
		}

		public TaskList(TaskList other)
		{
			Id = 0;
			Name = other.Name;
			DonePercentage = other.DonePercentage;
			CreationDate = other.CreationDate;
			GotStar = other.GotStar;

			taskList = other.taskList.Select(t => new ToDoTask(t)).ToList();

		}

		public TaskList(DateTime dt, int donePercentage)
		{
			this.CreationDate = dt;
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

		public void SetList(TaskList other)
		{
			// deep‐copy everything, including resetting the PK:
			var clone = new TaskList(other);
			this.Id = clone.Id;
			this.Name = clone.Name;
			this.CreationDate = clone.CreationDate;
			this.DonePercentage = clone.DonePercentage;
			this.GotStar = clone.GotStar;
			this.taskList = clone.taskList;
		}

	}
}
