using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _02_2
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			int x = 0, y = 0, aim = 0;
			foreach (var line in input)
			{
				switch (line.Split(" ")[0])
				{
					case "forward":
						x += Convert.ToInt32(line.Split(" ")[1]);
						y += aim * Convert.ToInt32(line.Split(" ")[1]);
						break;
					case "up":
						aim -= Convert.ToInt32(line.Split(" ")[1]);
						break;
					case "down":
						aim += Convert.ToInt32(line.Split(" ")[1]);
						break;

				}
			}
			Console.WriteLine(x * y);
		}

		private static List<string> ReadInput()
		{
			return File.ReadAllLines(@"input.txt").ToList();
		}
	}
}
