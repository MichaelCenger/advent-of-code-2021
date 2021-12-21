using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _21_2
{
	class Program
	{
		static void Main(string[] args)
		{
			long p1Wins = 0;
			long p2Wins = 0;
			Dictionary<(int p1Pos, int p2Pos, int p1Score, int p2Score), long> games = new Dictionary<(int p1Pos, int p2Pos, int p1Score, int p2Score), long>();
			List<int> possibleRolls = new List<int>();

			for (int i = 1; i <= 3; i++)
			{
				for (int j = 1; j <= 3; j++)
				{
					for (int k = 1; k <= 3; k++)
					{
						possibleRolls.Add(i + j + k);
					}
				}
			}

			games.Add((1, 2, 0, 0), 1);
			while (games.Count > 0)
			{
				Dictionary<(int p1Pos, int p2Pos, int p1Score, int p2Score), long> gamesCopy = new Dictionary<(int p1Pos, int p2Pos, int p1Score, int p2Score), long>();
				foreach (var game in games)
				{
					foreach (var roll in possibleRolls)
					{
						(int p1Pos, int p2Pos, int p1Score, int p2Score) state = game.Key;
						int newPos = (state.p1Pos + roll) % 10;
						if (newPos == 0)
						{
							newPos = 10;
						}
						state.p1Score += newPos;
						state.p1Pos = newPos;

						if (state.p1Score >= 21)
						{
							p1Wins += game.Value;
						}
						else
						{
							if (gamesCopy.ContainsKey(state))
							{
								gamesCopy[state] += game.Value;
							}
							else
							{
								gamesCopy.Add(state, game.Value);
							}
						}
					}
				}
				games = gamesCopy;
				gamesCopy = new Dictionary<(int p1Pos, int p2Pos, int p1Score, int p2Score), long>();
				foreach (var game in games)
				{
					foreach (var roll in possibleRolls)
					{
						(int p1Pos, int p2Pos, int p1Score, int p2Score) state = game.Key;
						int newPos = (state.p2Pos + roll) % 10;
						if (newPos == 0)
						{
							newPos = 10;
						}
						state.p2Score += newPos;
						state.p2Pos = newPos;

						if (state.p2Score >= 21)
						{
							p2Wins += game.Value;
						}
						else
						{
							if (gamesCopy.ContainsKey(state))
							{
								gamesCopy[state] += game.Value;
							}
							else
							{
								gamesCopy.Add(state, game.Value);
							}
						}
					}
				}
				games = gamesCopy;
			}
			Console.WriteLine(Math.Max(p1Wins, p2Wins));
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
