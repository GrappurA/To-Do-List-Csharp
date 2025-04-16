using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_C_
{
	internal class UserInfo
	{
		[Key]
		public int Id { get; set; }
		public List<Star> stars { get; private set; }
		public int starCount { get; set; }
		public double averageTasksDone { get; set; }
		public int daysInARow { get; set; }

		public UserInfo()
		{
			stars = new List<Star>();
		}

		public UserInfo(int id)
		{
			this.Id = id;
			stars = new List<Star>();
		}

		public void GiveStar(Star star)
		{
			this.stars.Add(star);
		}

		public void SetStarList(List<Star> stars)
		{
			this.stars = stars;
		}
	}
}