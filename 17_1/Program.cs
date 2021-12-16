using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _17_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			long result = 0;
			foreach (var line in input)
			{

			}

			Console.WriteLine(result);
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
