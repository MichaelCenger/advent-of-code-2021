using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _21_1
{
	class Program
	{
		static Random rng = new Random();
		static void Main(string[] args)
		{
			var input = ReadInput();
			int dieRollCount = 0;
			int p1Pos = 1;
			int p2Pos = 2;
			int p1Score = 0;
			int p2Score = 0;
			while (true)
			{
				int totalRoll = 0;
				for (int i = 0; i < 3; i++)
				{
					dieRollCount++;
					int roll = dieRollCount % 100;
					if (roll == 0)
					{
						roll = 100;
					}

					totalRoll += roll;
				}
				int newPos = (p1Pos + totalRoll) % 10;
				if (newPos == 0)
				{
					newPos = 10;
				}
				p1Score += newPos;
				p1Pos = newPos;
				if (p1Score >= 1000)
				{
					Console.WriteLine(p2Score * dieRollCount);
					break;
				}

				totalRoll = 0;
				for (int i = 0; i < 3; i++)
				{
					dieRollCount++;
					int roll = dieRollCount % 100;
					if (roll == 0)
					{
						roll = 100;
					}

					totalRoll += roll;
				}
				newPos = (p2Pos + totalRoll) % 10;
				if (newPos == 0)
				{
					newPos = 10;
				}
				p2Score += newPos;
				p2Pos = newPos;
				if (p2Score >= 1000)
				{
					Console.WriteLine(p1Score * dieRollCount);
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
