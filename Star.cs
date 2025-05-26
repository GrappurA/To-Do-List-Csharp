using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_C_
{
	public class Star
	{
		[Key]
		public int Id { get; set; }

		public int Size { get; set; }

		public DateTime earnDate { get; set; }

		public string? ListName { get; set; }

		public int ListId { get; set; }

		public Star() { }

		public Star(int Size, DateTime dt)
		{
			this.Size = Size;
			this.earnDate = dt;
		}

		public void SetSize(int Size)
		{
			if (Size < 0 | Size > 3) { return; }
			else { this.Size = Size; }
		}

		public int GetSize() { return this.Size; }


	}
}
