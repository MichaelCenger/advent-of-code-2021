using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _22_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			HashSet<(int x, int y, int z)> on = new HashSet<(int x, int y, int z)>();

			foreach (var line in input)
			{
				bool turnOn = line.Split(" ")[0] == "on";
				var coords = line.Split(" ")[1].Split(",");
				int xMin = Convert.ToInt32(coords[0].Split("..")[0]);
				int xMax = Convert.ToInt32(coords[0].Split("..")[1]);
				int yMin = Convert.ToInt32(coords[1].Split("..")[0]);
				int yMax = Convert.ToInt32(coords[1].Split("..")[1]);
				int zMin = Convert.ToInt32(coords[2].Split("..")[0]);
				int zMax = Convert.ToInt32(coords[2].Split("..")[1]);
				for (int x = xMin; x <= xMax; x++)
				{
					for (int y = yMin; y <= yMax; y++)
					{
						for (int z = zMin; z <= zMax; z++)
						{
							if (turnOn)
							{
								on.Add((x, y, z));
							}
							else
							{
								on.Remove((x, y, z));
							}
						}
					}
				}
				Console.WriteLine(on.Count);
			}
		}

		private static List<string> ReadInput()
		{
			return File.ReadAllLines(@"input.txt").ToList();
			//List<int> input = new List<int>();
			//string[] lines = File.ReadAllLines(@"input.txt");

			//foreach (string line in lines)
			//{
			//	input.Add(Convert.ToInt32(line));
			//}

			//return input;
		}
	}
}
