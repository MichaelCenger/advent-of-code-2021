using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _09_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			int[,] field = new int[input.Count, input[0].Length];
			for (int y = 0; y < input.Count; y++)
			{
				for (int x = 0; x < input[y].Length; x++)
				{
					field[y, x] = input[y][x]- '0';
				}
			}

			int heightSum = 0;
			for (int y = 0; y < input.Count; y++)
			{
				for (int x = 0; x < input[y].Length; x++)
				{
					int num = input[y][x] - '0';
					bool isLowPoint = true;
					if(y-1 >= 0)
					{
						if (field[y - 1,x] <= num)
						{
							isLowPoint = false;
						}
						
					}
					if (Convert.ToInt32(y + 1) < field.GetLength(0))
					{
						if (field[y + 1, x] <= num)
						{
							isLowPoint = false;
						}

					}
					if (Convert.ToInt32(x - 1) >= 0)
					{
						if (field[y, x-1] <= num)
						{
							isLowPoint = false;
						}

					}
					if (Convert.ToInt32(x + 1) < field.GetLength(1))
					{
						if (field[y, x+1]<= num)
						{
							isLowPoint = false;
						}

					}
					if (isLowPoint)
					{
						heightSum += num + 1;
					}
				}
			}
			Console.WriteLine(heightSum);
		}

		private static List<string> ReadInput()
		{
			return File.ReadAllLines(@"input.txt").ToList();
		}
	}
}
