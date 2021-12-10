using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _10_2
{
	class Program
	{
		static void Main(string[] args)
		{
			HashSet<char> openChars = new HashSet<char>() { '[', '{', '(', '<' };
			Dictionary<char, char> matchingChars = new Dictionary<char, char>() { { '[', ']' }, { '{', '}' }, { '(', ')' }, { '<', '>' } };
			Dictionary<char, int> scores = new Dictionary<char, int>() { { ']', 2 }, { '}', 3 }, { ')', 1 }, { '>', 4 } };
			var input = ReadInput();
			List<string> ToRemove = new List<string>();
			List<string> CompleteWith = new List<string>();
			foreach (var line in input)
			{
				Stack<char> openStack = new Stack<char>();
				for (int i = 0; i < line.Length; i++)
				{
					if (!openChars.Contains(line[i]))
					{
						if (matchingChars[openStack.Peek()] != line[i])
						{
							ToRemove.Add(line);
							openStack.Clear();
							break;
						}
						else
						{
							openStack.Pop();
						}
					}
					else
					{
						openStack.Push(line[i]);
					}
				}
				if (openStack.Count != 0)
				{
					CompleteWith.Add("");
				}
				foreach (char c in openStack)
				{
					CompleteWith[CompleteWith.Count - 1] += matchingChars[c];
				}
			}
			List<long> finalScores = new List<long>();
			foreach (var l in CompleteWith)
			{

				long score = 0;
				foreach (char c in l)
				{
					score *= 5;
					score += scores[c];
				}

				finalScores.Add(score);
			}
			finalScores.Sort();
			long result = finalScores[finalScores.Count / 2];
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
