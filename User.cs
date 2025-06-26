using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_C_
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		public List<Star> stars { get; set; }
		public int starCount { get; set; }
		public double averageTasksDone { get; set; }
		public int daysInARow { get; set; }
		public int MaxDaysInARow { get; set; }
		public bool TrainingEnabled { get; set; }

		public int? CurrentListId { get; set; }
		public TaskList? CurrentList { get; set; }

		public User()
		{
			stars = new List<Star>();
		}

		public User(int id)
		{
			this.Id = id;
			stars = new List<Star>();
		}



		public void GiveStar(Star star)
		{
			this.stars.Add(star);
		}


	}
}