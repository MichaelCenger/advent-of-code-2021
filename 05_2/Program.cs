using System;
using System.Collections.Generic;
using System.IO;

namespace _05_2
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			Dictionary<(int x, int y), int> coordinates = new Dictionary<(int x, int y), int>();
			foreach (var line in ReadInput())
			{
				var splitLine = Regex.Replace(line, " -> ", ",").Split(",");
				(int x, int y) start = (Convert.ToInt32(splitLine[0]), Convert.ToInt32(splitLine[1]));
				(int x, int y) end = (Convert.ToInt32(splitLine[2]), Convert.ToInt32(splitLine[3]));
				if (start.x > end.x)
				{
					int y = end.y;
					for (int x = end.x; x <= start.x; x++)
					{
						var pos = (x, y);
						if (!coordinates.ContainsKey(pos))
						{
							coordinates.Add(pos, 1);
						}
						else
						{
							coordinates[pos]++;
						}
						if (start.y > end.y)
						{
							y++;
						}
						else if (start.y < end.y)
						{
							y--;
						}
					}
					continue;
				}
				else if (start.x < end.x)
				{
					int y = start.y;
					for (int x = start.x; x <= end.x; x++)
					{
						var pos = (x, y);
						if (!coordinates.ContainsKey(pos))
						{
							coordinates.Add(pos, 1);
						}
						else
						{
							coordinates[pos]++;
						}
						if (start.y > end.y)
						{
							y--;
						}
						else if (start.y < end.y)
						{
							y++;
						}
					}
					continue;
				}
				if (start.y > end.y)
				{
					int x = end.x;
					for (int y = end.y; y <= start.y; y++)
					{
						var pos = (x, y);
						if (!coordinates.ContainsKey(pos))
						{
							coordinates.Add(pos, 1);
						}
						else
						{
							coordinates[pos]++;
						}
						if (start.x > end.x)
						{
							x++;
						}
						else if (start.x < end.x)
						{
							x--;
						}
					}
					continue;
				}
				else if (start.y < end.y)
				{
					int x = start.x;
					for (int y = start.y; y <= end.y; y++)
					{
						var pos = (x, y);
						if (!coordinates.ContainsKey(pos))
						{
							coordinates.Add(pos, 1);
						}
						else
						{
							coordinates[pos]++;
						}
						if (start.x > end.x)
						{
							x--;
						}
						else if (start.x < end.x)
						{
							x++;
						}
					}
					continue;
				}
			}


			int sum = 0;
			foreach (var entry in coordinates)
			{
				if (entry.Value >= 2)
				{
					sum++;
				}
			}
			Console.WriteLine(sum);
		}

		private static List<string> ReadInput()
		{
			return File.ReadAllLines(@"input.txt").ToList();
		}
	}
}
