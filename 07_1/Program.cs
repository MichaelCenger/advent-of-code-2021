using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _07_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			var positions = new List<int>();
			foreach (var pos in input)
			{
				positions.Add(Convert.ToInt32(pos));
			}
			int leastFuel = int.MaxValue;
			int leastFuelPosition = 0;
			foreach(var target in positions)
			{
				int currentTotalFuel = 0;
				foreach(var pos in positions)
				{
					currentTotalFuel += Math.Abs(target - pos);
					
				}
				if(currentTotalFuel < leastFuel)
				{
					leastFuel = currentTotalFuel;
					leastFuelPosition = target;
				}
			}
			Console.WriteLine(leastFuel);
		}

		private static List<string> ReadInput()
		{
			return File.ReadAllText(@"input.txt").Split(",").ToList();
		}
	}
}
