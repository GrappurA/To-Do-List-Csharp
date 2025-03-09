using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_C_
{
	class Task
	{
		public int Id { get; private set; }
		public string Name { get; set; }
		public bool Status { get; set; }

		public Task() { }

		public Task(int id, string name, bool status)
		{
			Id = id;
			Name = name;
			Status = status;
		}
	}
}
