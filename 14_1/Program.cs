using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace _14_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			Dictionary<char, long> Count = new Dictionary<char, long>();
			string polymer = input[0];
			Dictionary<string, char> instructions = new Dictionary<string, char>();
			for (int i = 2; i < input.Count; i++)
			{
				string searchTerm = input[i].Split("->")[0].Trim();
				char replaceWith = input[i].Split("->")[1].Trim()[0];
				instructions.Add(searchTerm,replaceWith);
			}
			for (int k = 0; k < 10; k++)
			{
				Dictionary<int, char> toInsert = new Dictionary<int, char>();
				Stopwatch sw = new Stopwatch();
				sw.Start();
				for (int l = 0; l < polymer.Length - 1; l++)
				{
					string s = polymer[l] + "" + polymer[l + 1];
					if (instructions.ContainsKey(s))
					{
						toInsert.Add(l + 1, instructions[s]);
						if (!Count.ContainsKey(instructions[s]))
						{
							Count.Add(instructions[s], 1);
						}
						else
						{
							Count[instructions[s]]++;
						}
					}
				}
				StringBuilder newPolymer = new StringBuilder(polymer.Length + toInsert.Count);

				for (int i = 0; i < polymer.Length + toInsert.Count; i++)
				{
					newPolymer.Append('X');

				}

				int offset = 0;
				for (int i = 0; i < polymer.Length + toInsert.Count; i++)
				{
					if (toInsert.ContainsKey(i))
					{
						newPolymer[i + offset] = toInsert[i];
						offset++;
					}
				}

				int originalIndex = 0;
				for (int i = 0; i < newPolymer.Length; i++)
				{
					if (newPolymer[i] == 'X')
					{
						newPolymer[i] = polymer[originalIndex];
						originalIndex++;
					}
				}
				polymer = newPolymer.ToString();
				Console.WriteLine(k);

			}

			for (int i = 0; i < input[0].Length; i++)
			{
				Count[input[0][i]]++;
			}
			long result = 0;

			long smallest = Int32.MaxValue;
			long largest = Int32.MinValue;
			foreach (var c in Count)
			{
				if (c.Value < smallest)
				{
					smallest = c.Value;
				}
				if (c.Value > largest)
				{
					largest = c.Value;
				}
			}
			Console.Write(largest - smallest);
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
