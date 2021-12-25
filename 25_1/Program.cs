using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _25_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			char[,] state = new char[input.Count, input[0].Length];
			for (int y = 0; y < state.GetLength(0); y++)
			{
				for (int x = 0; x < state.GetLength(1); x++)
				{
					state[y, x] = input[y][x];
				}
			}
			long moveCount = 1;
			while (true)
			{
				bool didMove = false;
				char[,] newState = new char[input.Count, input[0].Length];
				for (int y = 0; y < state.GetLength(0); y++)
				{
					for (int x = 0; x < state.GetLength(1); x++)
					{
						newState[y, x] = state[y, x];
					}
				}
				for (int y = 0; y < state.GetLength(0); y++)
				{
					for (int x = 0; x < state.GetLength(1); x++)
					{
						if (state[y, x] == '>')
						{
							int targetPosX = x + 1;
							if (targetPosX >= state.GetLength(1))
							{
								targetPosX = 0;
							}

							if (state[y, targetPosX] == '.')
							{
								newState[y, targetPosX] = '>';
								newState[y, x] = '.';
								didMove = true;
							}
						}
					}
				}
				for (int y = 0; y < state.GetLength(0); y++)
				{
					for (int x = 0; x < state.GetLength(1); x++)
					{
						state[y, x] = newState[y, x];
					}
				}
				for (int y = 0; y < state.GetLength(0); y++)
				{
					for (int x = 0; x < state.GetLength(1); x++)
					{
						if (state[y, x] == 'v')
						{
							int targetPosY = y + 1;
							if (targetPosY >= state.GetLength(0))
							{
								targetPosY = 0;
							}

							if (state[targetPosY, x] == '.')
							{
								newState[targetPosY, x] = 'v';
								newState[y, x] = '.';
								didMove = true;
							}
						}
					}
				}
				if (didMove)
				{
					state = newState;
					moveCount++;
				}
				else
				{
					break;
				}
			}
			Console.WriteLine(moveCount);
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
