using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _15_2
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			int[,] grid = new int[input.Count * 5, input.Count * 5];
			Dictionary<(int y, int x), (int risk, int riskSoFar)> nodes = new Dictionary<(int y, int x), (int risk, int riskSoFar)>();
			HashSet<(int y, int x)> visited = new HashSet<(int y, int x)>();
			for (int y = 0; y < input.Count; y++)
			{
				for (int x = 0; x < input[0].Length; x++)
				{
					for (int i = 0; i < 5; i++)
					{
						for (int j = 0; j < 5; j++)
						{
							int value = input[y][x] - '0' + i + j;
							if (value > 9)
							{
								value = value - 9;
							}
							grid[y + i * input.Count, x + j * input.Count] = value;
							nodes.Add((y + i * input.Count, x + j * input.Count), (value, 10000));
						}
					}
				}
			}
			int lowestRisk = Int32.MaxValue;
			(int y, int x) currentNode = (0, 0);
			nodes[currentNode] = (0, 0);
			while (true)
			{
				if (currentNode == (input.Count * 5 - 1, input.Count * 5 - 1))
				{
					Console.WriteLine(nodes[currentNode].riskSoFar);
					break;
				}
				for (int i = -1; i <= 1; i++)
				{
					for (int j = -1; j <= 1; j++)
					{
						int neighbourPosY = currentNode.Item1 + i;
						int neighbourPosX = currentNode.Item2 + j;
						if ((i == 0 || j == 0) && neighbourPosX >= 0 && neighbourPosX < grid.GetLength(0) && neighbourPosY >= 0 && neighbourPosY < grid.GetLength(0) && !visited.Contains((neighbourPosY, neighbourPosX)) && !(i == 0 && j == 0))
						{
							int risk = nodes[(neighbourPosY, neighbourPosX)].risk;
							int newTotalRisk = risk + nodes[currentNode].riskSoFar;
							if (newTotalRisk < nodes[(neighbourPosY, neighbourPosX)].riskSoFar)
							{
								nodes[(neighbourPosY, neighbourPosX)] = (risk, newTotalRisk);
							}
						}
					}
				}
				visited.Add(currentNode);
				int smallestRisk = Int32.MaxValue;
				foreach (var n in nodes)
				{
					if (!visited.Contains(n.Key) && n.Value.riskSoFar < smallestRisk)
					{
						smallestRisk = n.Value.riskSoFar;
						currentNode = n.Key;
					}
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
