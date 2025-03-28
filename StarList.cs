using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_C_
{
	internal class StarList
	{
		List<Star> starlist;

		public StarList()
		{
			starlist = new List<Star>();
		}

		public void AddStar(Star star,int size)
		{
			star.SetSize(size);
			starlist.Add(star);
		}
		public int GetSize()
		{
			return starlist.Count;
		}
			

	}
}
