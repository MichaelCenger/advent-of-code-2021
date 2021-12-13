using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _13_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			int paperSizeX = 0;
			int paperSizeY = 0;
			Dictionary<int, HashSet<int>> lines = new Dictionary<int, HashSet<int>>();
			foreach (var line in input)
			{
				if (String.IsNullOrEmpty(line))
				{
					break;
				}
				int x = Convert.ToInt32(line.Split(",")[0]);
				int y = Convert.ToInt32(line.Split(",")[1]);
				if (y > paperSizeY)
				{
					paperSizeY = y;
				}
				if (x > paperSizeX)
				{
					paperSizeX = x;
				}
				if (lines.ContainsKey(y))
				{
					lines[y].Add(x);
				}
				else
				{
					lines.Add(y, new HashSet<int>() { x });
				}
			}

			for (int y = 0; y <= paperSizeY; y++)
			{
				if (!lines.ContainsKey(y))
				{
					lines.Add(y, new HashSet<int>());
				}
			}

			foreach (var line in input)
			{
				if (!String.IsNullOrEmpty(line) && line[0] == 'f')
				{
					int fold = Convert.ToInt32(line.Split("=")[1]);
					char foldAlong = line.Split("=")[0][11];
					int offset = 1;
					if (foldAlong == 'y')
					{
						lines.Remove(fold);
						for (int y = fold + 1; y <= paperSizeY; y++)
						{
							lines[fold - offset].UnionWith(lines[y]);
							offset++;
							lines.Remove(y);
						}
						paperSizeY = fold - 1;

					}
					else
					{
						for (int y = 0; y <= paperSizeY; y++)
						{
							offset = 1;
							for (int x = fold + 1; x <= paperSizeX; x++)
							{
								HashSet<int> toRemove = new HashSet<int>();
								HashSet<int> toAdd = new HashSet<int>();
								if (lines[y].Contains(x))
								{
									int coord = x;
									if (coord >= fold + 1)
									{
										toAdd.Add(fold - offset);
										toRemove.Add(coord);
									}
									lines[y].UnionWith(toAdd);
									lines[y].ExceptWith(toRemove);
								}
								offset++;
							}
						}
						paperSizeX = fold - 1;
					}
					int dots = 0;
					foreach (var l in lines)
					{
						dots += l.Value.Count;
					}
					Console.WriteLine(dots);
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
