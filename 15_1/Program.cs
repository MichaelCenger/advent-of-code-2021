using System;
using System.Collections.Generic;
using System.IO;

namespace _15_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
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
