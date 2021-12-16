using System;
using System.IO;
using System.Linq;

namespace _16_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			string binaryString = String.Join(String.Empty, input.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

			int subPacketCount = 0;
			int versionSum = 0;
			for (int i = 0; i < binaryString.Length;)
			{
				if (i+3 >= binaryString.Length)
				{
					break;
				}
				int version = Convert.ToInt32(binaryString.Substring(i, 3), 2);
				i += 3;
				if (i+3 >= binaryString.Length)
				{
					break;
				}
				int typeId = Convert.ToInt32(binaryString.Substring(i, 3), 2);
				i += 3;
				if (typeId == 4)
				{
					string literalValueString = "";
					while (true)
					{
						string part = binaryString.Substring(i, 5);
						literalValueString += part.Substring(1);
						i += 5;
						if (part[0] == '0')
						{
							break;
						}

					}
					if (i >= binaryString.Length)
					{
						break;
					}
					if (subPacketCount > 0)
					{
						subPacketCount--;
					}
					if (i >= binaryString.Length)
					{
						break;
					}
					long literalValue = Convert.ToInt64(literalValueString, 2);
					Console.WriteLine("Value Packet with value: " + literalValue);
				}
				else
				{
					if (i + 1 >= binaryString.Length)
					{
						break;
					}
					int lengthTypeId = Convert.ToInt32(binaryString[i].ToString(),2);
					i++;
					if(lengthTypeId == 0)
					{
						if (subPacketCount > 0)
						{
							subPacketCount--;
						}
						if (i + 15 >= binaryString.Length)
						{
							break;
						}
						int bitLength = Convert.ToInt32(binaryString.Substring(i,15), 2);
						i += 15;
						if (i >= binaryString.Length)
						{
							break;
						}
					}
					else
					{
						if (subPacketCount > 0)
						{
							subPacketCount--;
						}
						if (i + 11 >= binaryString.Length)
						{
							break;
						}
						subPacketCount += Convert.ToInt32(binaryString.Substring(i, 11), 2);
						Console.WriteLine("OperatorPacket with: " + subPacketCount + " subpackets");
						i += 11;
						if (i >= binaryString.Length)
						{
							break;
						}
					}
				}
				versionSum += version;
			}
			Console.WriteLine(versionSum);
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
