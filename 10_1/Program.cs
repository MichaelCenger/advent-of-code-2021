using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _10_1
{
	class Program
	{
		static void Main(string[] args)
		{
			HashSet<char> openChars = new HashSet<char>() { '[', '{', '(', '<' };
			Dictionary<char, char> matchingChars = new Dictionary<char, char>() { { '[', ']' }, { '{', '}' }, { '(', ')' }, { '<', '>' } };
			Dictionary<char, int> scores = new Dictionary<char, int>() { { ']',57 }, { '}', 1197 }, { ')', 3 }, { '>', 25137 } };
			var input = ReadInput();
			int score = 0;
			foreach (var line in input)
			{
				Stack<char> openStack = new Stack<char>();
				for (int i = 0; i < line.Length; i++)
				{
					if (!openChars.Contains(line[i]))
					{
						if (matchingChars[openStack.Peek()] != line[i])
						{
							score += scores[line[i]];
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

			}
			Console.WriteLine(score);
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
