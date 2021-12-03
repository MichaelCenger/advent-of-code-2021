using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _03_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			string gamma = "", epsilon = "";
			for (int i = 0; i < input[0].Length; i++)
			{
				int count1 = 0, count0 = 0;
				for (int j = 0; j < input.Count; j++)
				{
					if (input[j][i] == '0')
					{
						count0++;
					}
					else
					{
						count1++;
					}
				}
				if (count1 > count0)
				{
					gamma += '1';
					epsilon += '0';
				}
				else
				{
					gamma += '0';
					epsilon += '1';
				}
			}
			int a= Convert.ToInt32(gamma, 2); 
			int b= Convert.ToInt32(epsilon, 2);
			Console.WriteLine(a * b);
		}

		private static List<string> ReadInput()
		{
			return File.ReadAllLines(@"input.txt").ToList();
		}
	}
}
