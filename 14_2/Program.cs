using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _14_2
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			Dictionary<char, long> Count = new Dictionary<char, long>();
			Dictionary<string, char> instructions = new Dictionary<string, char>();
			for (int i = 2; i < input.Count; i++)
			{
				string searchTerm = input[i].Split("->")[0].Trim();
				char replaceWith = input[i].Split("->")[1].Trim()[0];
				instructions.Add(searchTerm, replaceWith);
			}
			Dictionary<string, long> pairs = new Dictionary<string, long>();
			for (int l = 0; l < input[0].Length - 1; l++)
			{
				string s = input[0][l] + "" + input[0][l + 1];
				if (!pairs.ContainsKey(s))
				{
					pairs.Add(s, 1);
				}
				else
				{
					pairs[s]++;
				}
				if (Count.ContainsKey(input[0][l]))
				{
					Count[input[0][l]]++;
				}
				else
				{
					Count.Add(input[0][l], 1);
				}
			}
			if (Count.ContainsKey(input[0][19]))
			{
				Count[input[0][19]]++;
			}
			else
			{
				Count.Add(input[0][19], 1);
			}

			for (int k = 0; k < 40; k++)
			{
				Dictionary<string, long> newPairs = new Dictionary<string, long>(pairs);
				List<(string, long)> toRemove = new List<(string, long)>();
				foreach (var instruction in instructions)
				{
					if (pairs.ContainsKey(instruction.Key))
					{
						long currentCount = pairs[instruction.Key];

						if (Count.ContainsKey(instruction.Value))
						{
							Count[instruction.Value] += currentCount;
						}
						else
						{
							Count.Add(instruction.Value, currentCount);
						}
						string s1 = instruction.Key[0] + "" + instruction.Value;
						if (newPairs.ContainsKey(s1))
						{
							newPairs[s1] += currentCount;
						}
						else
						{
							newPairs.Add(s1, currentCount);
						}
						string s2 = instruction.Value + "" + instruction.Key[1];
						if (newPairs.ContainsKey(s2))
						{
							newPairs[s2] += currentCount;
						}
						else
						{
							newPairs.Add(s2, currentCount);
						}
						toRemove.Add((instruction.Key, currentCount));
					}
				}
				for (int i = 0; i < toRemove.Count; i++)
				{
					newPairs[toRemove[i].Item1] -= toRemove[i].Item2;
					if (newPairs[toRemove[i].Item1] == 0)
					{
						newPairs.Remove(toRemove[i].Item1);
					}
				}
				pairs = newPairs;
			}


			long smallest = long.MaxValue;
			long largest = 0;
			foreach (var c in Count)
			{
				Console.WriteLine(c.Key + " occurs " + c.Value + " times");
				if (c.Value < smallest)
				{
					smallest = c.Value;
				}
				if (c.Value > largest)
				{
					largest = c.Value;
				}
			}
			Console.WriteLine((largest - smallest));
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
