using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _06_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			List<int> fish = new List<int>();
			foreach(var num in input)
			{
				fish.Add(Convert.ToInt32(num)); 
			}
			for ( int i = 0;i < 80; i++)
			{
				var fishToAdd = new List<int>();
				for(int j = 0; j< fish.Count;j++)
				{
					int num = fish[j];
					num--;
					if (num == -1)
					{
						num = 6;
						fishToAdd.Add(8);
					}
					fish[j] = num;
				}
				fish.AddRange(fishToAdd);
			}
			Console.WriteLine(fish.Count);
		}

		private static List<string> ReadInput()
		{
			return File.ReadAllText(@"input.txt").Split(",").ToList();
		}
	}
}
