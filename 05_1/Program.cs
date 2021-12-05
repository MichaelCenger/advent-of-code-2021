using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _05_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			Dictionary<(int x, int y), int> coordinates = new Dictionary<(int x, int y), int>();
			foreach(var line in ReadInput())
			{
				var splitLine = Regex.Replace(line, " -> ", ",").Split(",");
				(int x, int y) start = (Convert.ToInt32(splitLine[0]), Convert.ToInt32(splitLine[1]));
				(int x, int y) end = (Convert.ToInt32(splitLine[2]), Convert.ToInt32(splitLine[3]));
				if(start.x != end.x && start.y != end.y)
				{
					continue;
				}
				if(start.x > end.x)
				{
					for (int x = end.x; x <= start.x; x++)
					{
						var pos = (x, start.y);
						if (!coordinates.ContainsKey(pos))
						{
							coordinates.Add(pos, 1);
						}
						else
						{
							coordinates[pos]++;
						}
					}
					continue;
				}
				else if(start.x < end.x)
				{
					for (int x = start.x; x <= end.x; x++)
					{
						var pos = (x, start.y);
						if (!coordinates.ContainsKey(pos))
						{
							coordinates.Add(pos, 1);
						}
						else
						{
							coordinates[pos]++;
						}
					}
					continue;
				}
				if (start.y > end.y)
				{
					for (int y = end.y; y <= start.y; y++)
					{
						var pos = (start.x, y);
						if (!coordinates.ContainsKey(pos))
						{
							coordinates.Add(pos, 1);
						}
						else
						{
							coordinates[pos]++;
						}
					}
					continue;
				}
				else if (start.y < end.y)
				{
					for (int y = start.y; y <= end.y; y++)
					{
						var pos = (start.x, y);
						if (!coordinates.ContainsKey(pos))
						{
							coordinates.Add(pos, 1);
						}
						else
						{
							coordinates[pos]++;
						}
					}
					continue;
				}

			}

			int sum = 0;
			foreach(var entry in coordinates)
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
