using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _11_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			int[,] a = new int[input.Count, input[0].Length];
			for (int y = 0; y < input.Count; y++)
			{
				for (int x = 0; x < input[y].Length; x++)
				{
					a[y, x] = input[y][x] - '0';
				}
			}

			
			int flashes = 0;
			for (int k = 0; k < 100;k++)
			{
				for (int y = 0; y < input.Count; y++)
				{
					for (int x = 0; x < input[y].Length; x++)
					{
						a[y, x]++;
					}
				}
				Console.WriteLine(k);
				bool canFlash = true;
				HashSet<(int x, int y)> hasFlashed = new HashSet<(int x, int y)>();
				while (canFlash)
				{
					canFlash = false;
					for (int y = 0; y < input.Count; y++)
					{
						for (int x = 0; x < input[y].Length; x++)
						{
							if (a[y, x] > 9)
							{
								flashes++;
								hasFlashed.Add((x, y));
								for (int i = -1; i <= 1; i++)
								{
									for (int j = -1; j <= 1; j++)
									{
										if (!(i == 0 && j == 0))
										{
											if (((y + i) < a.GetLength(0)) && ((y + i) >= 0) && ((x + j) < a.GetLength(1)) && ((x + j) >= 0))
											{
												if (a[y + i, x + j] + 1 > 9)
												{
													canFlash = true;
												}
												a[y + i, x + j]++;
											}
										}
									}
								}
							}
						}
					}


					for (int y = 0; y < input.Count; y++)
					{
						string line = "";
						for (int x = 0; x < input[y].Length; x++)
						{
							line += a[y, x];
						}
						Console.WriteLine(line);
					}
					
					foreach (var entry in hasFlashed)
					{
						a[entry.y, entry.x] = 0;
					}
					Console.WriteLine("\n\n");
				}

				
			}
			Console.WriteLine(flashes);
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
