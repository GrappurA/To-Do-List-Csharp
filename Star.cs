using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_C_
{
	class Star
	{
		public int Size { get; private set; }

		public Star() { }

		public Star(int Size)
		{
			this.Size = Size;
		}

		public void SetSize(int Size)
		{
			if (Size < 0 || Size > 3) { return; }
			else { this.Size = Size; }

		}


	}
}
