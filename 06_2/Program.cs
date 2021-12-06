using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _06_2
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			Dictionary<int, long> fish = new Dictionary<int, long>();
			fish.Add(0, 0);
			fish.Add(1, 0);
			fish.Add(2, 0);
			fish.Add(3, 0);
			fish.Add(4, 0);
			fish.Add(5, 0);
			fish.Add(6, 0);
			fish.Add(7, 0);
			fish.Add(8, 0);
			foreach (var num in input)
			{
				fish[Convert.ToInt32(num)]++;
			}
			for (int i = 0; i < 256; i++)
			{
				var copy = new Dictionary<int, long>(fish);
				copy[0] = fish[1];
				copy[1] = fish[2];
				copy[2] = fish[3];
				copy[3] = fish[4];
				copy[4] = fish[5];
				copy[5] = fish[6];
				copy[6] = fish[7] + fish[0];
				copy[7] = fish[8];
				copy[8] = fish[0];
				fish = new Dictionary<int, long>(copy);
			}
			long sum = 0;
			foreach (var f in fish)
			{
				sum += f.Value;
			}
			Console.WriteLine(sum);
		}

		private static List<string> ReadInput()
		{
			return File.ReadAllText(@"input.txt").Split(",").ToList();
		}
	}
}
