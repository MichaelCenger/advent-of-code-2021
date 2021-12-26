using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _24_1
{
	class Program
	{
		static List<int> A = new List<int> { 10, 12, 10, 12, 11, -16, 10, -11, -13, 13, -8, -1, -4, -14 };
		static List<int> B = new List<int> { 12, 7, 8, 8, 15, 12, 8, 13, 3, 13, 3, 9, 4, 13 };
		static List<int> C = new List<int> { 1, 1, 1, 1, 1, 26, 1, 26, 26, 1, 26, 26, 26, 26 };

		static List<string> instructionBlock = new List<string>();
		static Dictionary<(long target, int block), List<(long z, long w)>> mem = new Dictionary<(long target, int block), List<(long z, long w)>>();
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

		}

		private static void Solve(long target, int block, List<long> number)
		{
			if (block < 0)
			{
				string num = "";
				for (int i = number.Count - 1; i >= 0; i--)
				{
					num += number[i];
				}

				using (StreamWriter w = File.AppendText("result.txt"))
				{
					w.WriteLine(num);
				}
				return;
			}
			if (mem.ContainsKey((target, block)))
			{
				foreach (var sol in mem[(target, block)])
				{
					var newList = new List<long>(number);
					newList.Add(sol.w);
					Solve(sol.z, block - 1, newList);
				}
				return;
			}
			List<(long z, long w)> solutions = new List<(long z, long w)>();
			long maxZ = (target + 1) * C[block];
			if(block == 0)
			{
				maxZ = 1;
			}
			for (long z = 0; z < maxZ; z++)
			{
				for (int w = 9; w >= 1; w--)
				{
					long newZ = CalculateBlock(w, z, block);
					if (newZ == target)
					{
						solutions.Add((z, w));
					}
				}
			}
			if (!mem.ContainsKey((target, block)))
			{
				mem.Add((target, block), new List<(long z, long w)>(solutions));
			}

			foreach (var sol in solutions)
			{
				var newList = new List<long>(number);
				newList.Add(sol.w);
				Solve(sol.z, block - 1, newList);
			}
		}

		private static long CalculateBlock(int w, long z, int block)
		{
			long newZ = z / C[block];

			if ((z % 26 + A[block]) == w)
			{
				return newZ;
			}
			return 26 * newZ + w + B[block];
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
