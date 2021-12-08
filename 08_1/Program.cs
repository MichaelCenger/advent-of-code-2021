using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _08_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			int sum = 0;

			foreach(var line in input)
			{
				string output = line.Split("|")[1];
				foreach(var o in output.Split(" "))
				{
					if((o.Length == 2) || (o.Length == 3) || (o.Length == 4) || (o.Length == 7))
					{
						sum++;
					}
				}
			}
			Console.WriteLine(sum);
		}

		private static List<string> ReadInput()
		{
			return File.ReadAllLines(@"input.txt").ToList();
		}
	}
}
