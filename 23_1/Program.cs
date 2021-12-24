using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _23_1
{
	class Program
	{
		class Amphipod
		{
			public int TargetColumn;
			public char Type;
			public (int y, int x) Position;
			public int StepsTaken = 0;

			public Amphipod(int targetColumn, char type, (int y, int x) position)
			{
				TargetColumn = targetColumn;
				Type = type;
				Position = position;
			}

			public void MoveTo((int y, int x) newPos, bool backtrack)
			{
				int steps = Math.Abs(newPos.x - Position.x) + Math.Abs(newPos.y - Position.y);
				state[Position.y, Position.x] = '.';
				Position = newPos;
				state[Position.y, Position.x] = Type;
			}

			public List<(int y, int x, int cost)> GetPossibleMoves()
			{
				List<(int y, int x, int cost)> moves = new List<(int y, int x, int cost)>();
				for (int y = 0; y < state.GetLength(0); y++)
				{
					for (int x = 0; x < state.GetLength(1); x++)
					{
						//Empty
						if (state[y, x] != '.')
						{
							continue;
						}
						//Not allowed to move from hallway to hallway (Rule #3)
						if (y == 1 && Position.y == 1)
						{
							continue;
						}
						//Not allowed to stop in front of burrow on hallway (Rule #1)
						if (y == 1 && (x == 3 || x == 5 || x == 7 || x == 9))
						{
							continue;
						}
						//Not allowed to move into other burrows (Rule #2)
						if ((x == 3 || x == 5 || x == 7 || x == 9) && TargetColumn != x)
						{
							continue;
						}
						//Not allowed to move into own burrows if there are wrong amphipods in there (Rule #2)
						if (TargetColumn == x)
						{
							bool allowed = true;
							int count = 0;
							for (int checkY = 0; checkY < state.GetLength(0); checkY++)
							{
								if (state[checkY, x] == Type)
								{
									count++;
								}
								if (state[checkY, x] != '.' && state[checkY, x] != '#' && state[checkY, x] != Type)
								{
									allowed = false;
									break;
								}
							}
							if (!allowed || (count < 1 && y == 2))
							{
								continue;
							}
						}
						int cost;
						if (HasPath(Position, (y, x), out cost))
						{
							moves.Add((y, x, cost));
						}
					}
				}
				return moves;
			}

			public bool HasPath((int y, int x) start, (int y, int x) end, out int cost)
			{
				Stack<(int y, int x)> positions = new Stack<(int y, int x)>();
				HashSet<(int y, int x)> visited = new HashSet<(int y, int x)>();
				HashSet<(int y, int x)> path = new HashSet<(int y, int x)>();
				positions.Push(start);
				while (positions.Count != 0)
				{
					var current = positions.Pop();
					if (visited.Contains(current))
					{
						continue;
					}
					visited.Add(current);
					if (current == end)
					{
						cost = path.Count;
						if (Type == 'A')
						{
							cost *= 1;
						}
						if (Type == 'B')
						{
							cost *= 10;
						}
						if (Type == 'C')
						{
							cost *= 100;
						}
						if (Type == 'D')
						{
							cost *= 1000;
						}
						return true;
					}
					if (current.x + 1 < state.GetLength(1))
					{
						(int y, int x) neighbourPos = (current.y, current.x + 1);
						if (state[neighbourPos.y, neighbourPos.x] == '.' && !visited.Contains(neighbourPos))
						{
							positions.Push(neighbourPos);
							path.Add(current);
						}
					}
					if (current.x - 1 >= 0)
					{
						(int y, int x) neighbourPos = (current.y, current.x - 1);
						if (state[neighbourPos.y, neighbourPos.x] == '.' && !visited.Contains(neighbourPos))
						{
							positions.Push(neighbourPos);
							path.Add(current);
						}
					}
					if (current.y - 1 >= 0)
					{
						(int y, int x) neighbourPos = (current.y - 1, current.x);
						if (state[neighbourPos.y, neighbourPos.x] == '.' && !visited.Contains(neighbourPos))
						{
							positions.Push(neighbourPos);
							path.Add(current);
						}
					}
					if (current.y + 1 < state.GetLength(0))
					{
						(int y, int x) neighbourPos = (current.y + 1, current.x);
						if (state[neighbourPos.y, neighbourPos.x] == '.' && !visited.Contains(neighbourPos))
						{
							positions.Push(neighbourPos);
							path.Add(current);
						}
					}
				}
				cost = 0;
				return false;
			}
		}
		static char[,] state = new char[5, 13];
		static List<Amphipod> Amphipods = new List<Amphipod>();
		static int lowestCost = Int32.MaxValue;
		static HashSet<int> costs = new HashSet<int>();
		static void Main(string[] args)
		{
			var input = ReadInput();
			for (int y = 0; y < input.Count; y++)
			{
				for (int x = 0; x < input[0].Length; x++)
				{
					state[y, x] = input[y][x];
					if (input[y][x] == 'A')
					{
						Amphipods.Add(new Amphipod(3, 'A', (y, x)));
					}
					if (input[y][x] == 'B')
					{
						Amphipods.Add(new Amphipod(5, 'B', (y, x)));
					}
					if (input[y][x] == 'C')
					{
						Amphipods.Add(new Amphipod(7, 'C', (y, x)));
					}
					if (input[y][x] == 'D')
					{
						Amphipods.Add(new Amphipod(9, 'D', (y, x)));
					}
				}
			}
			int totalCost = 0;
			Move(totalCost, GetBoard());
			Console.WriteLine(lowestCost);
		}

		static void Move(int totalCost, string path)
		{
			//path += GetBoard();
			if (IsDone())
			{
				if (totalCost < lowestCost)
				{
					lowestCost = totalCost;
					Console.WriteLine("=======================================");
					Console.WriteLine("\n\n\n\n" + totalCost);
					Console.WriteLine(path);
				}
			}
			foreach (var a in Amphipods)
			{
				if (a.Position.x == a.TargetColumn)
				{
					bool isInFinalPosition = true;
					for (int y = 0; y < state.GetLength(0); y++)
					{
						if (state[y, a.Position.x] != '.' && state[y, a.Position.x] != '#' && state[y, a.Position.x] != a.Type)
						{
							isInFinalPosition = false;
							break;
						}
					}
					if (isInFinalPosition)
					{
						continue;
					}
				}


				foreach (var possibleMove in a.GetPossibleMoves())
				{
					(int y, int x) posBeforeMove = a.Position;
					//Console.WriteLine(a.Type + "Moves From " + posBeforeMove + " To " + possibleMove);
					//path += "\nCost Before:" + totalCost + "\n";
					//path += "\n" + a.Type + "Moves From " + posBeforeMove + " To " + (possibleMove.y, possibleMove.x) + "\n";
					a.MoveTo((possibleMove.y, possibleMove.x), false);

					//path += GetBoard();
					//path += "\nCost After:" + (totalCost + possibleMove.cost) + "\n";
					Move(totalCost + possibleMove.cost, path);
					//path += "\n" + a.Type + "Backtracks From " + a.Position + " To " + posBeforeMove + "\n";
					//Console.WriteLine(a.Type + "Backtracks From " + a.Position + " To " + posBeforeMove);
					a.MoveTo(posBeforeMove, true);
					//path += GetBoard();
				}
			}
		}

		static bool IsDone()
		{
			bool done = true;
			foreach (var a in Amphipods)
			{
				if (a.Position.x != a.TargetColumn)
				{
					done = false;
					break;
				}
			}
			return done;
		}

		static int GetTotalCost()
		{
			int sum = 0;
			foreach (var a in Amphipods)
			{
				if (a.Type == 'A')
				{
					sum += a.StepsTaken;
				}
				if (a.Type == 'B')
				{
					sum += a.StepsTaken * 10;
				}
				if (a.Type == 'C')
				{
					sum += a.StepsTaken * 100;
				}
				if (a.Type == 'D')
				{
					sum += a.StepsTaken * 1000;
				}
			}
			return sum;
		}

		static string GetBoard()
		{
			string result = "";
			for (int y = 0; y < state.GetLength(0); y++)
			{
				string line = "";
				for (int x = 0; x < state.GetLength(1); x++)
				{
					line += state[y, x];
				}
				result += line + "\n";
			}
			return result;
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
