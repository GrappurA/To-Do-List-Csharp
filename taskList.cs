using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_C_
{
	internal class TaskList
	{
		bool gotStar;
		List<Task> taskList;
		string pathToList;
		string pathToInfo;
		
		public TaskList()
		{
			taskList = new List<Task>();
			gotStar = false;
		}

		public void setPathToList(string path)
		{
			this.pathToList = path;			
		}
		
		public void setPathToInfo(string path)
		{
			this.pathToInfo = path;			
		}

		public string GetPathToList()
		{
			return this.pathToList;
		}
		
		public string GetPathToInfo()
		{
			return this.pathToInfo;
		}

		public void SetGotStarStatus(bool gotStar)
		{
			this.gotStar = gotStar;
		}
		
		public bool GetGotStarStatus()
		{
			return this.gotStar;
		}

		public void AddElement(Task elem)
		{
			taskList.Add(elem);
		}

		public void RemoveElement(Task elem)
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

		public List<Task> GetList()
		{
			return taskList;
		}

		public void SetList(List<Task> taskList)
		{
			this.taskList = taskList;
		}

	}
}
