using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_C_
{
	public class ToDoTask
	{

		[Browsable(false)] [Key] public int Id { get; set; }
		public int Position { get; set; }
		public string Name { get; set; }
		public bool Status { get; set; }
		public DateTime DueDate { get; set; }

		public ToDoTask() { }

		public ToDoTask(int pos, string name, bool status, DateTime dueDate)
		{
			this.Position = pos;
			Name = name;
			Status = status;
			DueDate = dueDate;
		}

		public ToDoTask(string name, bool status, DateTime dueDate)
		{
			Name = name;
			Status = status;
			DueDate = dueDate;
		}
	}
}
