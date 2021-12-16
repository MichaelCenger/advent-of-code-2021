using System;
using System.Collections.Generic;
using System.IO;

namespace _16_2
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	namespace _16_1
	{
		class Program
		{
			static int versionSum = 0;
			static Stack<string> instructionStack = new Stack<string>();
			static void Main(string[] args)
			{

				var input = ReadInput();
				string binaryString = String.Join(String.Empty, input.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

				List<int> packets = new List<int>();

				ResolvePackage(binaryString, 0, binaryString.Length - 1);
				Console.WriteLine(versionSum);
				while (instructionStack.Count > 0)
				{
					Console.Write(instructionStack.Pop() + " ");
				}
				//Final Calculation was done by Hand. Because Why Not :)
			}

			private static string IdToInstruction(int id)
			{
				switch (id)
				{
					case 0:
						return "+";
					case 1:
						return "*";
					case 2:
						return "min";
					case 3:
						return "max";
					case 5:
						return ">";
					case 6:
						return "<";
					case 7:
						return "=";
				}
				return "X";
			}

			private static int ResolvePackage(string binaryString, int start, int end)
			{
				int i = start;
				while (i < end)
				{
					if (i + 3 >= binaryString.Length)
					{
						return i;
					}
					int version = Convert.ToInt32(binaryString.Substring(i, 3), 2);
					i += 3;
					if (i + 3 >= binaryString.Length)
					{
						return i;
					}
					int typeId = Convert.ToInt32(binaryString.Substring(i, 3), 2);
					i += 3;
					if (typeId == 4)
					{
						i += ResolveLiteralValue(binaryString.Substring(i));
					}
					else
					{
						if (i + 1 >= binaryString.Length)
						{
							return i;
						}
						int lengthTypeId = Convert.ToInt32(binaryString[i].ToString(), 2);
						i++;
						if (lengthTypeId == 0)
						{
							if (i + 15 >= binaryString.Length)
							{
								return i;
							}
							int bitLength = Convert.ToInt32(binaryString.Substring(i, 15), 2);
							i += 15;
							Console.WriteLine("OperatorPacket with: " + bitLength + " bits");
							instructionStack.Push(")");
							ResolvePackage(binaryString, i, i + bitLength);
							instructionStack.Push("(");
							i += bitLength;
						}
						else
						{
							if (i + 11 >= binaryString.Length)
							{
								return i;
							}
							int subPacketCount = Convert.ToInt32(binaryString.Substring(i, 11), 2);
							i += 11;
							Console.WriteLine("OperatorPacket with: " + subPacketCount + " subpackets");
							instructionStack.Push(")");
							for (int k = 0; k < subPacketCount; k++)
							{
								i += ResolvePackageSingle(binaryString, i);
							}
							instructionStack.Push("(");
						}
					}
					versionSum += version;
					if (typeId != 4)
					{
						instructionStack.Push(IdToInstruction(typeId));
					}
				}
				return i;
			}

			private static int ResolvePackageSingle(string binaryString, int start)
			{
				int i = start; if (i + 3 >= binaryString.Length)
				{
					return i;
				}
				int version = Convert.ToInt32(binaryString.Substring(i, 3), 2);
				i += 3;
				if (i + 3 >= binaryString.Length)
				{
					return i;
				}
				int typeId = Convert.ToInt32(binaryString.Substring(i, 3), 2);
				i += 3;
				if (typeId == 4)
				{
					i += ResolveLiteralValue(binaryString.Substring(i));
				}
				else
				{
					if (i + 1 >= binaryString.Length)
					{
						return i;
					}
					int lengthTypeId = Convert.ToInt32(binaryString[i].ToString(), 2);
					i++;
					if (lengthTypeId == 0)
					{
						if (i + 15 >= binaryString.Length)
						{
							return i;
						}
						int bitLength = Convert.ToInt32(binaryString.Substring(i, 15), 2);
						i += 15;
						Console.WriteLine("OperatorPacket with: " + bitLength + " bits");
						instructionStack.Push(")");
						ResolvePackage(binaryString, i, i + bitLength);
						instructionStack.Push("(");
						i += bitLength;
					}
					else
					{
						if (i + 11 >= binaryString.Length)
						{
							return i;
						}
						int subPacketCount = Convert.ToInt32(binaryString.Substring(i, 11), 2);
						i += 11;
						Console.WriteLine("OperatorPacket with: " + subPacketCount + " subpackets");
						instructionStack.Push(")");
						for (int k = 0; k < subPacketCount; k++)
						{
							i += ResolvePackageSingle(binaryString, i);
						}
						instructionStack.Push("(");
					}
				}
				versionSum += version;
				if (typeId != 4)
				{
					instructionStack.Push(IdToInstruction(typeId));
				}
				return i - start;
			}

			private static int ResolveLiteralValue(string input)
			{
				string literalValueString = "";
				int i = 0;
				while (true)
				{
					string part = input.Substring(i, 5);
					literalValueString += part.Substring(1);
					i += 5;
					if (part[0] == '0')
					{
						break;
					}

				}
				long literalValue = Convert.ToInt64(literalValueString, 2);
				Console.WriteLine("Value Packet with value: " + literalValue);
				instructionStack.Push(literalValue.ToString());
				return i;
			}

			private static string ReadInput()
			{
				return File.ReadAllText(@"input.txt");
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

}
