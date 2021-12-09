using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _09_2
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			HashSet<(int, int)> used = new HashSet<(int, int)>();
			int[,] field = new int[input.Count, input[0].Length];
			for (int y = 0; y < input.Count; y++)
			{
				for (int x = 0; x < input[y].Length; x++)
				{
					field[y, x] = input[y][x] - '0';
				}
			}

			List<int> basins = new List<int>();

			for (int y = 0; y < field.GetLength(0); y++)
			{
				for (int x = 0; x < field.GetLength(1); x++)
				{
					int num = field[y, x];
					if (num != 9 && !used.Contains((y, x)))
					{
						Stack<(int y, int x)> floodStack = new Stack<(int y, int x)>();
						floodStack.Push((y, x));
						used.Add((y, x));
						int basinSize = 0;
						while (floodStack.Count > 0)
						{
							var currentCoordinates = floodStack.Pop();
							basinSize++;
							if (currentCoordinates.y - 1 >= 0)
							{
								if (!used.Contains((currentCoordinates.y - 1, currentCoordinates.x)))
								{
									if (field[currentCoordinates.y - 1, currentCoordinates.x] != 9)
									{
										used.Add((currentCoordinates.y - 1, currentCoordinates.x));
										floodStack.Push((currentCoordinates.y - 1, currentCoordinates.x));
									}
								}
							}
							if (currentCoordinates.y + 1 < field.GetLength(0))
							{
								if (!used.Contains((currentCoordinates.y + 1, currentCoordinates.x)))
								{
									if (field[currentCoordinates.y + 1, currentCoordinates.x] != 9)
									{
										used.Add((currentCoordinates.y + 1, currentCoordinates.x));
										floodStack.Push((currentCoordinates.y + 1, currentCoordinates.x));
									}
								}

							}
							if (currentCoordinates.x - 1 >= 0)
							{
								if (!used.Contains((currentCoordinates.y, currentCoordinates.x - 1)))
								{
									if (field[currentCoordinates.y, currentCoordinates.x - 1] != 9)
									{
										used.Add((currentCoordinates.y, currentCoordinates.x - 1));
										floodStack.Push((currentCoordinates.y, currentCoordinates.x - 1));
									}
								}
							}
							if (currentCoordinates.x + 1 < field.GetLength(1))
							{
								if (!used.Contains((currentCoordinates.y, currentCoordinates.x + 1)))
								{
									if (field[currentCoordinates.y, currentCoordinates.x + 1] != 9)
									{
										used.Add((currentCoordinates.y, currentCoordinates.x + 1));
										floodStack.Push((currentCoordinates.y, currentCoordinates.x + 1));
									}
								}
							}
						}
						basins.Add(basinSize);
					}
				}
			}

			basins.Sort();
			int product = 1;
			for (int i = basins.Count - 1; i > basins.Count - 4; i--)
			{
				product *= basins[i];
			}
			Console.WriteLine(product);
		}

		private static List<string> ReadInput()
		{
			return File.ReadAllLines(@"input.txt").ToList();
		}
	}
}
