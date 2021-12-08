using System;
using System.Collections.Generic;
using System.IO;

namespace _08_2
{
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			int sum = 0;
			foreach (var line in input)
			{
				Dictionary<char, char> mapping = new Dictionary<char, char>();
				Dictionary<string, int> mappedNumbers = new Dictionary<string, int>();
				string signal = line.Split("|")[0];
				string one = "", four = "", seven = "";
				foreach (var signalNumber in signal.Split(" "))
				{
					if ((signalNumber.Length == 3))
					{
						seven = signalNumber;
					}
					else if (signalNumber.Length == 2)
					{
						one = signalNumber;
					}
					else if (signalNumber.Length == 4)
					{
						four = signalNumber;
					}
				}
				string three = "", six = "";
				foreach (var signalNumber in signal.Split(" "))
				{
					if (signalNumber.Length == 5)
					{
						bool isThree = true;
						foreach (var c in seven)
						{
							if (!signalNumber.Contains(c))
							{
								isThree = false;
							}
						}
						if (isThree)
						{
							three = signalNumber;
						}
					}
					else if (signalNumber.Length == 6)
					{
						int k = 0;
						char matchedChar = 'x';
						char notMatchedChar = 'x';
						foreach (var c in one)
						{
							if (signalNumber.Contains(c))
							{
								matchedChar = c;
								k++;
							}
							else
							{
								notMatchedChar = c;
							}
						}
						if (k == 1)
						{
							six = signalNumber;
							mapping.Add(matchedChar, 'f');
							mapping.Add(notMatchedChar, 'c');
						}
					}
				}

				string two = "", five = "";
				foreach (var signalNumber in signal.Split(" "))
				{
					if (signalNumber.Length == 5 && signalNumber != three)
					{
						foreach (var c in signalNumber)
						{
							if (mapping.ContainsKey(c) && mapping[c] == 'c')
							{
								two = signalNumber;
								continue;
							}
							else if (mapping.ContainsKey(c) && mapping[c] == 'f')
							{
								five = signalNumber;
								continue;
							}
						}
					}
				}
				HashSet<char> twoChars = new HashSet<char>();
				foreach (var c in two)
				{
					twoChars.Add(c);
				}
				foreach (var c in five)
				{
					if (!twoChars.Contains(c) && !(mapping.ContainsKey(c) && mapping[c] == 'f'))
					{
						mapping.Add(c, 'b');
					}
				}
				HashSet<char> fiveChars = new HashSet<char>();
				foreach (var c in five)
				{
					fiveChars.Add(c);
				}
				foreach (var c in two)
				{
					if (!fiveChars.Contains(c) && !(mapping.ContainsKey(c) && mapping[c] == 'c'))
					{
						mapping.Add(c, 'e');
					}
				}
				char d = 'x';
				foreach (var c in four)
				{
					if (!mapping.ContainsKey(c))
					{
						mapping.Add(c, 'd');
						d = c;
					}
				}
				string zero = "";
				foreach (var signalNumber in signal.Split(" "))
				{
					if (signalNumber.Length == 6)
					{
						bool isZero = true;
						foreach (var c in signalNumber)
						{
							if (c == d)
							{
								isZero = false;
							}
						}
						if (isZero)
						{
							zero = signalNumber;
							break;
						}
					}
				}
				foreach (char c in zero)
				{
					if (!mapping.ContainsKey(c) && !seven.Contains(c))
					{
						mapping.Add(c, 'g');
					}
				}
				foreach (char c in seven)
				{
					if (!mapping.ContainsKey(c))
					{
						mapping.Add(c, 'a');
					}
				}
				string numbers = line.Split("|")[1].Trim();
				List<int> digits = new List<int>();
				foreach (var num in numbers.Split(" "))
				{
					if (num.Length == 2)
					{
						digits.Add(1);
					}
					else if (num.Length == 3)
					{
						digits.Add(7);
					}
					else if (num.Length == 4)
					{
						digits.Add(4);
					}
					else if (num.Length == 7)
					{
						digits.Add(8);
					}
					else if (num.Length == 5)
					{
						bool isThree = true;
						foreach (var c in num)
						{
							if (mapping[c] == 'b')
							{
								digits.Add(5);
								isThree = false;
							}
							else if (mapping[c] == 'e')
							{
								digits.Add(2);
								isThree = false;
							}
						}
						if (isThree)
						{
							digits.Add(3);
						}
					}
					else if (num.Length == 6)
					{
						bool isZero = true;
						bool isSix = true;
						bool isNine = true;
						foreach (var c in num)
						{
							if (mapping[c] == 'd')
							{
								isZero = false;
							}
							if (mapping[c] == 'e')
							{
								isNine = false;
							}
							if (mapping[c] == 'c')
							{
								isSix = false;
							}
						}
						if (isZero)
						{
							digits.Add(0);
						}
						if (isSix)
						{
							digits.Add(6);
						}
						if (isNine)
						{
							digits.Add(9);
						}
					}
				}
				int decodedNumber = 0;
				int i = 1000;
				foreach (var a in digits)
				{
					decodedNumber += a * i;
					i /= 10;
				}
				sum += decodedNumber;

			}

			Console.WriteLine(sum);
		}

		private static List<string> ReadInput()
		{
			return File.ReadAllLines(@"input.txt").ToList();
		}
	}
}
