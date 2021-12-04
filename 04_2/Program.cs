using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _04_2
{
	class Board
	{
		bool[,] marked = new bool[5, 5];
		public int[,] numbers = new int[5, 5];
		public int Mark(int number)
		{
			if (number == 16)
			{
				Print();
				PrintMarked();
			}
			for (int x = 0; x < 5; x++)
			{
				for (int y = 0; y < 5; y++)
				{
					if (numbers[x, y] == number)
					{
						marked[x, y] = true;
					}
				}
			}
			return CheckIfWon();
		}

		public int CheckIfWon()
		{
			for (int x = 0; x < 5; x++)
			{
				bool won = true;
				for (int y = 0; y < 5; y++)
				{
					if (marked[x, y] == false)
					{
						won = false;
						break;
					}
				}
				if (won)
				{
					return GetScore();
				}
			}

			for (int x = 0; x < 5; x++)
			{
				bool won = true;
				for (int y = 0; y < 5; y++)
				{
					if (marked[y, x] == false)
					{
						won = false;
						break;
					}
				}
				if (won)
				{
					return GetScore();
				}
			}

			return -1;
		}

		public int GetScore()
		{
			int sum = 0;
			for (int x = 0; x < 5; x++)
			{
				for (int y = 0; y < 5; y++)
				{
					if (marked[x, y] == false)
					{
						sum += numbers[x, y];
					}
				}
			}
			return sum;
		}

		public void Print()
		{
			for (int x = 0; x < 5; x++)
			{
				string line = "";
				for (int y = 0; y < 5; y++)
				{
					line += numbers[x, y] + " ";
				}
				Console.WriteLine(line);
			}
			Console.WriteLine("\n\n");
		}

		public void PrintMarked()
		{
			for (int x = 0; x < 5; x++)
			{
				string line = "";
				for (int y = 0; y < 5; y++)
				{
					if (marked[x, y])
					{
						line += "X";
					}
					else
					{
						line += "0";
					}
				}
				Console.WriteLine(line);
			}
			Console.WriteLine("\n\n");
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			var numbers = input[0].Split(',').Select(Int32.Parse).ToList();
			input.RemoveAt(0);
			List<Board> boards = new List<Board>();
			Board currentBoard = null;
			int j = 0;
			foreach (var line in input)
			{
				if (String.IsNullOrEmpty(line))
				{
					currentBoard = new Board();
					j = 0;
					boards.Add(currentBoard);
					continue;
				}
				int i = 0;
				for (int x = 0; x < line.Length; x += 3)
				{
					int num = Convert.ToInt32(("" + line[x] + line[x + 1]).Trim());

					currentBoard.numbers[i, j] = num;
					i++;
				}
				j++;
			}

			foreach (var num in numbers)
			{
				List<Board> boardsToRemove = new List<Board>();
				foreach (var board in boards)
				{
					int sum = board.Mark(num);
					if (sum != -1)
					{
						boardsToRemove.Add(board);
						if (boards.Count == 1)
						{
							Console.WriteLine(num + "|" + sum * num);
						}
					}
				}

				boards.RemoveAll(b => boardsToRemove.Contains(b));
			}
		}

		private static List<string> ReadInput()
		{
			return File.ReadAllLines(@"input.txt").ToList();
		}
	}
}
