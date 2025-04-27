using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_C_
{
	class Task
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool Status { get; set; }
		public DateTime DueDate { get; set; }

		public Task() { }

		public Task(int id, string name, bool status, DateTime dueDate)
		{
			Id = id;
			Name = name;
			Status = status;
			DueDate = dueDate;
		}
	}
}
