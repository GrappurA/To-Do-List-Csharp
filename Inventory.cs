using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_C_
{
	class Inventory
	{
		List<Star> stars;

		public Inventory()
		{
			stars = new List<Star>();
		}
		public void addElement(Star element)
		{
			stars.Add(element);
		}
		public void removeElement(Star element)
		{
			stars.Remove(element);
		}



	}
}
