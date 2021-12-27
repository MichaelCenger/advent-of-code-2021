using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _23_2
{
	class Program
	{
		class Node
		{
			public Node Previous = null;
			public List<Edge> Edges = new List<Edge>();
			public int Distance = Int32.MaxValue;
			public char[,] Board;
		}

		class Edge
		{
			public int Cost = 0;
			public Node Other = null;

			public Edge(int cost, Node other)
			{
				Cost = cost;
				Other = other;
			}
		}

		class Amphipod
		{
			public int TargetColumn;
			public char Type;
			public (int y, int x) Position;
			public int StepsTaken = 0;
			public int Moves = 0;

			public Amphipod(int targetColumn, char type, (int y, int x) position, int moves)
			{
				TargetColumn = targetColumn;
				Type = type;
				Position = position;
				Moves = moves;
			}

			public void MoveTo((int y, int x) newPos, char[,] state)
			{
				int steps = Math.Abs(newPos.x - Position.x) + Math.Abs(newPos.y - Position.y);
				state[Position.y, Position.x] = '.';
				Position = newPos;
				state[Position.y, Position.x] = Type;
				Moves++;
			}

			public List<(int y, int x, int cost)> GetPossibleMoves(char[,] state)
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
							if (!allowed || (y == 2 && count != 3) || (y == 3 && count != 2) || (y == 4 && count != 1) || (y == 5 && count != 0))
							{
								continue;
							}
						}
						int cost;
						if (HasPath(Position, (y, x), out cost, state))
						{
							moves.Add((y, x, cost));
						}
					}
				}
				return moves;
			}

			public bool HasPath((int y, int x) start, (int y, int x) end, out int cost, char[,] state)
			{
				Dictionary<(int y, int x), int> unexplored = new Dictionary<(int y, int x), int>();

				Stack<(int y, int x)> positions = new Stack<(int y, int x)>();
				HashSet<(int y, int x)> visited = new HashSet<(int y, int x)>();
				positions.Push(start);
				while (positions.Count != 0)
				{
					var current = positions.Pop();
					unexplored.Add(current, Int32.MaxValue);
					visited.Add(current);
					if ((current.x + 1) < state.GetLength(1))
					{
						(int y, int x) neighbourPos = (current.y, current.x + 1);
						if (state[neighbourPos.y, neighbourPos.x] == '.' && !visited.Contains(neighbourPos))
						{
							positions.Push(neighbourPos);
						}
					}
					if ((current.x - 1) >= 0)
					{
						(int y, int x) neighbourPos = (current.y, current.x - 1);
						if (state[neighbourPos.y, neighbourPos.x] == '.' && !visited.Contains(neighbourPos))
						{
							positions.Push(neighbourPos);
						}
					}
					if ((current.y - 1) >= 0)
					{
						(int y, int x) neighbourPos = (current.y - 1, current.x);
						if (state[neighbourPos.y, neighbourPos.x] == '.' && !visited.Contains(neighbourPos))
						{
							positions.Push(neighbourPos);
						}
					}
					if ((current.y + 1) < state.GetLength(0))
					{
						(int y, int x) neighbourPos = (current.y + 1, current.x);
						if (state[neighbourPos.y, neighbourPos.x] == '.' && !visited.Contains(neighbourPos))
						{
							positions.Push(neighbourPos);
						}
					}
				}

				unexplored[start] = 0;
				int minCost = Int32.MaxValue;
				bool foundPath = false;
				while (unexplored.Count != 0)
				{
					int minDistance = Int32.MaxValue;
					(int y, int x) currentPos = (0, 0);
					foreach (var node in unexplored)
					{
						if (node.Value < minDistance)
						{
							currentPos = node.Key;
							minDistance = node.Value;
						}
					}
					int myVal = unexplored[currentPos];
					unexplored.Remove(currentPos);
					if (unexplored.ContainsKey((currentPos.y + 1, currentPos.x)))
					{
						int newDistance = myVal + 1;
						if (newDistance < unexplored[(currentPos.y + 1, currentPos.x)])
						{
							unexplored[(currentPos.y + 1, currentPos.x)] = newDistance;
							if ((currentPos.y + 1, currentPos.x) == end)
							{
								foundPath = true;
								if (newDistance < minCost)
								{
									minCost = newDistance;
								}
							}
						}
					}
					if (unexplored.ContainsKey((currentPos.y - 1, currentPos.x)))
					{
						int newDistance = myVal + 1;
						if (newDistance < unexplored[(currentPos.y - 1, currentPos.x)])
						{
							unexplored[(currentPos.y - 1, currentPos.x)] = newDistance;
							if ((currentPos.y - 1, currentPos.x) == end)
							{
								foundPath = true;
								if (newDistance < minCost)
								{
									minCost = newDistance;
								}
							}
						}
					}
					if (unexplored.ContainsKey((currentPos.y, currentPos.x + 1)))
					{
						int newDistance = myVal + 1;
						if (newDistance < unexplored[(currentPos.y, currentPos.x + 1)])
						{
							unexplored[(currentPos.y, currentPos.x + 1)] = newDistance;
							if ((currentPos.y, currentPos.x + 1) == end)
							{
								foundPath = true;
								if (newDistance < minCost)
								{
									minCost = newDistance;
								}
							}
						}
					}
					if (unexplored.ContainsKey((currentPos.y, currentPos.x - 1)))
					{
						int newDistance = myVal + 1;
						if (newDistance < unexplored[(currentPos.y, currentPos.x - 1)])
						{
							unexplored[(currentPos.y, currentPos.x - 1)] = newDistance;
							if ((currentPos.y, currentPos.x - 1) == end)
							{
								foundPath = true;
								if (newDistance < minCost)
								{
									minCost = newDistance;
								}
							}
						}
					}
				}

				cost = 0;
				switch (Type)
				{
					case 'A':
						cost = minCost;
						break;
					case 'B':
						cost = minCost * 10;
						break;
					case 'C':
						cost = minCost * 100;
						break;
					case 'D':
						cost = minCost * 1000;
						break;
				}
				return foundPath;
			}
			public Amphipod CreateCopy()
			{
				return new Amphipod(TargetColumn, Type, Position, Moves);
			}
		}
		static Node targetNode;
		static void Main(string[] args)
		{
			char[,] state = new char[7, 13];
			List<Amphipod> amphipods = new List<Amphipod>();
			var input = ReadInput();
			for (int y = 0; y < input.Count; y++)
			{
				for (int x = 0; x < input[0].Length; x++)
				{
					state[y, x] = input[y][x];
					if (input[y][x] == 'A')
					{
						amphipods.Add(new Amphipod(3, 'A', (y, x), 0));
					}
					if (input[y][x] == 'B')
					{
						amphipods.Add(new Amphipod(5, 'B', (y, x), 0));
					}
					if (input[y][x] == 'C')
					{
						amphipods.Add(new Amphipod(7, 'C', (y, x), 0));
					}
					if (input[y][x] == 'D')
					{
						amphipods.Add(new Amphipod(9, 'D', (y, x), 0));
					}
				}
			}
			int totalCost = 0;
			Node startNode = new Node();
			startNode.Board = state;
			Dictionary<string, Node> nodes = new Dictionary<string, Node>();
			nodes.Add(GetBoard(state), startNode);
			CreateGraph(state, amphipods, startNode, nodes);
			FindLowestCost(startNode, targetNode, nodes);
		}

		static void CreateGraph(char[,] state, List<Amphipod> amphipods, Node currentNode, Dictionary<string, Node> nodes)
		{
			if (IsDone(amphipods))
			{
				targetNode = currentNode;
			}
			foreach (var a in amphipods)
			{
				if ((a.Position.x == a.TargetColumn))
				{
					bool isInFinalPosition = true;
					for (int checkY = 0; checkY < state.GetLength(0); checkY++)
					{
						if (state[checkY, a.Position.x] != '.' && state[checkY, a.Position.x] != '#' && state[checkY, a.Position.x] != a.Type)
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

				foreach (var possibleMove in a.GetPossibleMoves(state))
				{
					char[,] stateCopy = new char[7, 13];
					for (int y = 0; y < state.GetLength(0); y++)
					{
						for (int x = 0; x < state.GetLength(1); x++)
						{
							stateCopy[y, x] = state[y, x];
						}
					}
					List<Amphipod> amphipodCopy = new List<Amphipod>();
					foreach (var amph in amphipods)
					{
						Amphipod copy = amph.CreateCopy();
						if (amph == a)
						{
							copy.MoveTo((possibleMove.y, possibleMove.x), stateCopy);
						}
						amphipodCopy.Add(copy);
					}
					if (nodes.ContainsKey(GetBoard(stateCopy)))
					{
						currentNode.Edges.Add(new Edge(possibleMove.cost, nodes[GetBoard(stateCopy)]));
						continue;
					}
					Node newNode = new Node();
					newNode.Board = stateCopy;
					currentNode.Edges.Add(new Edge(possibleMove.cost, newNode));

					nodes.Add(GetBoard(stateCopy), newNode);
					CreateGraph(stateCopy, amphipodCopy, newNode, nodes);
				}
			}
		}

		static void FindLowestCost(Node startNode, Node targetNode, Dictionary<string, Node> nodes)
		{
			int lowestCost = int.MaxValue;
			HashSet<Node> unexplored = new HashSet<Node>();
			startNode.Distance = 0;
			foreach (var node in nodes)
			{
				unexplored.Add(node.Value);
			}
			while (unexplored.Count != 0)
			{
				int minDistance = Int32.MaxValue;
				Node currentNode = null;
				foreach (var node in unexplored)
				{
					if (node.Distance < minDistance)
					{
						currentNode = node;
						minDistance = node.Distance;
					}
				}
				unexplored.Remove(currentNode);
				foreach (var edge in currentNode.Edges)
				{
					if (unexplored.Contains(edge.Other))
					{
						int newDistance = currentNode.Distance + edge.Cost;
						if (newDistance < edge.Other.Distance)
						{
							edge.Other.Distance = newDistance;
							edge.Other.Previous = currentNode;
							if (edge.Other == targetNode)
							{
								if (newDistance < lowestCost)
								{
									lowestCost = newDistance;
								}
								Console.WriteLine(newDistance);
								Node printNode = targetNode;
								while (printNode.Previous != null)
								{
									Console.WriteLine(GetBoard(printNode.Previous.Board));
									printNode = printNode.Previous;
								}

							}
						}
					}
				}
			}

			Console.WriteLine(lowestCost);
			//Stack<(char[,] board, List<Amphipod> amphipods)> states = new Stack<(char[,], List<Amphipod>)>();
			//HashSet<char[,]> visited = new HashSet<char[,]>();
			//HashSet<(char[,] state, int cost)> path = new HashSet<(char[,] state, int cost)>();
			//states.Push((startingState, startingAmphipods));
			//int paths = 0;
			//while (states.Count != 0)
			//{
			//	paths++;
			//	var current = states.Pop();
			//	if (visited.Contains(current.board))
			//	{
			//		continue;
			//	}
			//	visited.Add(current.board);
			//	foreach (var a in current.amphipods)
			//	{

			//	}
			//}
			//Console.WriteLine(paths);


			//path += GetBoard();
			//if (IsDone())
			//{
			//	if (totalCost < lowestCost)
			//	{
			//		lowestCost = totalCost;
			//		Console.WriteLine("=======================================");
			//		Console.WriteLine("\n\n\n\n" + totalCost);
			//		Console.WriteLine(path);
			//	}
			//}

		}

		static bool IsDone(List<Amphipod> amphipods)
		{
			bool done = true;
			foreach (var a in amphipods)
			{
				if (a.Position.x != a.TargetColumn)
				{
					done = false;
					break;
				}
			}
			return done;
		}

		//static int GetTotalCost()
		//{
		//	int sum = 0;
		//	foreach (var a in Amphipods)
		//	{
		//		if (a.Type == 'A')
		//		{
		//			sum += a.StepsTaken;
		//		}
		//		if (a.Type == 'B')
		//		{
		//			sum += a.StepsTaken * 10;
		//		}
		//		if (a.Type == 'C')
		//		{
		//			sum += a.StepsTaken * 100;
		//		}
		//		if (a.Type == 'D')
		//		{
		//			sum += a.StepsTaken * 1000;
		//		}
		//	}
		//	return sum;
		//}

		static string GetBoard(char[,] state)
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
