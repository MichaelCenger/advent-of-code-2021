using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _24_1
{
	class Program
	{


		static List<string> instructionBlock = new List<string>();
		static long largestNumber = 0;
		static long smallestNumber = long.MaxValue;
		static Dictionary<(long target, int block), (long z, long w)> mem = new Dictionary<(long target, int block), (long z, long w)>();
		static void Main(string[] args)
		{
			var instructions = ReadInput();
			string block = "";
			foreach (var line in instructions)
			{
				if (string.IsNullOrEmpty(line))
				{
					instructionBlock.Add(block.Substring(0, block.Length - 1));
					block = "";
				}
				else
				{
					block += line + ",";
				}
			}
			instructionBlock.Add(block.Substring(0, block.Length - 1));
			Solve(0, 13, new List<long>());
			Console.WriteLine(largestNumber);
			Console.WriteLine(smallestNumber);
		}

		private static void Solve(long target, int digit, List<long> number)
		{
			if (digit < 0)
			{
				string num = "";
				for (int i = number.Count - 1; i >= 0; i--)
				{
					num += number[i];
				}
				Console.WriteLine(num);
				largestNumber = Math.Max(largestNumber, Convert.ToInt64(num));
				smallestNumber = Math.Min(smallestNumber, Convert.ToInt64(num));
				return;
			}
			Dictionary<char, long> register = new Dictionary<char, long>();

			for (int z = 10; z < 1000000; z++)
			{
				for (int w = 9; w >= 1; w--)
				{
					if (mem.ContainsKey((target, digit)))
					{
						var sol = mem[(target, digit)];
						var newList = new List<long>(number);
						newList.Add(sol.w);
						Solve(sol.z, digit - 1, newList);
						return;
					}
					register['x'] = 0;
					register['y'] = 0;
					register['z'] = z;
					register['w'] = w;
					foreach (var line in instructionBlock[digit].Split(","))
					{
						string[] instruction = line.Split(" ");
						string op = instruction[0];
						char location = instruction[1][0];
						switch (op)
						{
							case "add":
								string bInstruction = instruction[2];
								long b = 0;
								if (long.TryParse(bInstruction, out b))
								{

								}
								else
								{
									b = register[bInstruction[0]];
								}
								register[location] += b;
								break;
							case "mul":
								bInstruction = instruction[2];
								b = 0;
								if (long.TryParse(bInstruction, out b))
								{

								}
								else
								{
									b = register[bInstruction[0]];
								}
								register[location] *= b;
								break;
							case "div":
								bInstruction = instruction[2];
								b = 0;
								if (long.TryParse(bInstruction, out b))
								{

								}
								else
								{
									b = register[bInstruction[0]];
								}
								register[location] /= b;
								break;
							case "mod":
								bInstruction = instruction[2];
								b = 0;
								if (long.TryParse(bInstruction, out b))
								{

								}
								else
								{
									b = register[bInstruction[0]];
								}
								register[location] = register[location] % b;
								break;
							case "eql":
								bInstruction = instruction[2];
								b = 0;
								if (long.TryParse(bInstruction, out b))
								{

								}
								else
								{
									b = register[bInstruction[0]];
								}
								register[location] = Convert.ToInt64(register[location] == b);
								break;
						}
					}

					if (register['z'] == target)
					{
						var newList = new List<long>(number);
						newList.Add(w);

						if (!mem.ContainsKey((target, digit)))
						{
							mem.Add((target, digit), (register['z'], w));
						}
						Solve(register['z'], digit - 1, newList);
					}
				}
			}
		}

		private static List<string> ReadInput()
		{
			return File.ReadAllLines(@"input.txt").ToList();
			//List<long> input = new List<long>();
			//string[] lines = File.ReadAllLines(@"input.txt");

			//foreach (string line in lines)
			//{
			//	input.Add(Convert.Tolong32(line));
			//}

			//return input;
		}
	}
}
