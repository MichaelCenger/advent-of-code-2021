using System;
using System.Collections.Generic;
using System.IO;

namespace _01_2
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();

			List<int> tripletSums = new List<int>();
			for (int i = 0; i < input.Count; i++)
			{
				if ((i + 2) < input.Count)
				{
					tripletSums.Add(input[i] + input[i + 1] + input[i + 2]);
				}
			}

			int previousNumber = int.MaxValue;
			int increasingCount = 0;
			foreach (var number in tripletSums)
			{
				if (number > previousNumber)
				{
					increasingCount++;
				}
				previousNumber = number;
			}

			Console.WriteLine(increasingCount);
		}

		private static List<int> ReadInput()
		{
			List<int> input = new List<int>();
			string[] lines = File.ReadAllLines(@"input.txt");

			foreach (string line in lines)
			{
				input.Add(Convert.ToInt32(line));
			}

			return input;
		}
	}
}
