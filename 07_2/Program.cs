using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _07_2
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			var positions = new List<int>();
			int maxNum = 0;
			foreach (var pos in input)
			{
				positions.Add(Convert.ToInt32(pos));
				if (Convert.ToInt32(pos) > maxNum)
				{
					maxNum = Convert.ToInt32(pos);
				}
			}
			int leastFuel = int.MaxValue;
			int leastFuelPosition = 0;
			for (int target = 0; target <= maxNum; target++)
			{
				int currentTotalFuel = 0;
				foreach (var pos in positions)
				{
					int cost = 0;
					for (int j = 0; j <= Math.Abs(target - pos); j++)
					{
						cost += j;
					}
					currentTotalFuel += cost;
				}
				if (currentTotalFuel < leastFuel)
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
