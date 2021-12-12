using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _12_2
{
	class Cave
	{
		public string name = "";
		public List<Cave> neighbours = new List<Cave>();
		public bool IsSmallCave => Char.IsLower(name, 0);
	}
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			Dictionary<string, Cave> caves = new Dictionary<string, Cave>();
			Cave startCave = null;
			Cave endCave = null;
			foreach (var line in input)
			{
				string nameA = line.Split("-")[0];
				string nameB = line.Split("-")[1];
				Cave caveA = new Cave();
				caveA.name = nameA;
				Cave caveB = new Cave();
				caveB.name = nameB;


				if (startCave == null)
				{
					if (nameA == "start")
					{
						startCave = caveA;
					}
					else if (nameB == "start")
					{
						startCave = caveB;
					}
				}
				if (caves.ContainsKey(nameA))
				{
					if (caves.ContainsKey(nameB))
					{
						caves[nameA].neighbours.Add(caves[nameB]);
						caves[nameB].neighbours.Add(caves[nameA]);
					}
					else
					{
						caveB.neighbours.Add(caves[nameA]);
						caves[nameA].neighbours.Add(caveB);
						caves.Add(nameB, caveB);
					}
				}
				else
				{
					caves.Add(nameA, caveA);
					if (caves.ContainsKey(nameB))
					{
						caveA.neighbours.Add(caves[nameB]);
						caves[nameB].neighbours.Add(caveA);
					}
					else
					{
						caves.Add(nameB, caveB);
						caveB.neighbours.Add(caveA);
						caveA.neighbours.Add(caveB);
					}
				}
			}


			int result = 0;
			CheckPaths(startCave, ref result, new Dictionary<Cave, int>(), false);

			Console.WriteLine(result);
		}

		public static void CheckPaths(Cave currentCave, ref int count, Dictionary<Cave, int> visited, bool visitedSmallTwice)
		{
			Dictionary<Cave, int> visitedCopy = new Dictionary<Cave, int>(visited);
			if (currentCave.IsSmallCave)
			{
				if (visitedCopy.ContainsKey(currentCave))
				{
					visitedCopy[currentCave]++;
					visitedSmallTwice = true;
				}
				else
				{
					visitedCopy.Add(currentCave, 1);
				}
			}
			if (currentCave.name == "end")
			{
				count++;
				return;
			}
			foreach (Cave c in currentCave.neighbours)
			{
				if (!visitedCopy.ContainsKey(c) || (!visitedSmallTwice && visitedCopy.ContainsKey(c) && (visitedCopy[c] < 2) && c.name != "start" && c.name != "end"))
				{
					CheckPaths(c, ref count, visitedCopy, visitedSmallTwice);
				}
			}
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
