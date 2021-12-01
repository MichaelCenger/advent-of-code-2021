using System;
using System.Collections.Generic;
using System.IO;

namespace _01_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			int previousNumber = int.MaxValue;
			int increasingCount = 0;
			foreach(var number in input)
			{
				if(number > previousNumber)
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
