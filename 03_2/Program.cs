using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _03_2
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			var inputCopy = new List<string>(input);
			int index = 0;
			while (input.Count != 1)
			{
				string mostCommon = "0", leastCommon = "0";
				int count1 = 0, count0 = 0;
				for (int j = 0; j < input.Count; j++)
				{
					if (input[j][index] == '0')
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
					mostCommon = "1";
				}
				else if (count1 < count0)
				{
					leastCommon = "1";
				}
				else
				{
					mostCommon = "1";
				}
				var ToRemove = new List<int>();
				for (int k = 0; k < input.Count; k++)
				{
					if (input[k][index] != mostCommon[0])
					{
						ToRemove.Add(k);
					}
				}
				ToRemove.Reverse();
				foreach (var r in ToRemove)
				{
					input.RemoveAt(r);
				}

				index++;
			}
			int a = Convert.ToInt32(input[0], 2);

			input = inputCopy;

			index = 0;
			while (input.Count != 1)
			{
				string mostCommon = "0", leastCommon = "0";
				int count1 = 0, count0 = 0;
				for (int j = 0; j < input.Count; j++)
				{
					if (input[j][index] == '0')
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
					mostCommon = "1";
				}
				else if (count1 < count0)
				{
					leastCommon = "1";
				}
				else
				{
					leastCommon = "0";
				}
				var ToRemove = new List<int>();
				for (int k = 0; k < input.Count; k++)
				{
					if (input[k][index] != leastCommon[0])
					{
						ToRemove.Add(k);
					}
				}
				ToRemove.Reverse();
				foreach (var r in ToRemove)
				{
					input.RemoveAt(r);
				}

				index++;
			}
			int b = Convert.ToInt32(input[0], 2);
			Console.WriteLine(b);
			Console.WriteLine(a * b);
		}

		private static List<string> ReadInput()
		{
			return File.ReadAllLines(@"input.txt").ToList();
		}
	}
}
