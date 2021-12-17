using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace _17_1
{
	class Program
	{
		static int maxY = Int32.MinValue;

		static void Main(string[] args)
		{
			var input = ReadInput();
			long result = 0;
			for (int velX = -500; velX < 500; velX++)
			{
				for (int velY = -500; velY < 500; velY++)
				{
					Launch(velX, velY);
				}
			}
			Console.WriteLine(maxY);
		}

		public static void Launch(int velX, int velY)
		{
			int x = 0;
			int y = 0;

			int myMaxY = Int32.MinValue;
			for (int i = 0; i < 1000; i++)
			{
				x += velX;
				y += velY;
				velY -= 1;
				if (velX > 0)
				{
					velX -= 1;
				}
				else if (velX < 0)
				{
					velX += 1;
				}
				if (y > myMaxY)
				{
					myMaxY = y;
				}
				if (x >= 201 && x <= 230 && y >= -99 && y <= -65)
				{
					if (myMaxY > maxY)
					{
						maxY = myMaxY;
					}
					break;
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
