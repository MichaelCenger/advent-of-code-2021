using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _18_2
{
	class Program
	{
		static void Main(string[] args)
		{
			int result = 0;
			var input = ReadInput();
			int largest = Int32.MinValue;
			List<string> numbers = new List<string>();
			for (int i = 0; i < input.Count; i++)
			{
				for (int j = 0; j < input.Count; j++)
				{
					if (j != i)
					{
						numbers.Add("[" + input[i] + "," + input[j] + "]");
					}
				}
			}
			foreach (var n in numbers)
			{
				StringBuilder addedNumber = new StringBuilder(n);
				while (true)
				{
					int bracketCount = 0;
					bool hasExploded = false;
					StringBuilder copy = new StringBuilder(addedNumber.ToString());
					for (int j = 0; j < addedNumber.Length; j++)
					{
						if (addedNumber[j] == '[')
						{
							bracketCount++;
							if (bracketCount >= 5)
							{
								string pair = "";
								for (int k = j + 1; k < addedNumber.Length; k++)
								{
									if (addedNumber[k] == ']')
									{
										break;
									}

									pair += addedNumber[k];
								}

								copy.Remove(j, pair.Length + 2);
								copy.Insert(j, "0");
								int change = pair.Length + 1;
								int leftNumber = Convert.ToInt32(pair.Split(',')[0]);
								int rightNumber = Convert.ToInt32(pair.Split(',')[1]);

								for (int k = j + pair.Length + 1; k < addedNumber.Length; k++)
								{
									if (Char.IsDigit(addedNumber[k]))
									{
										string numToReplace = "";
										for (int l = k; l < addedNumber.Length; l++)
										{
											if (!Char.IsDigit(addedNumber[l]))
											{
												break;
											}
											numToReplace += addedNumber[l];
										}
										int num = Convert.ToInt32(numToReplace);
										copy.Remove(k - change, numToReplace.Length);
										copy.Insert(k - change, (num + rightNumber).ToString());
										break;
									}
								}
								for (int k = j - 1; k >= 0; k--)
								{
									if (Char.IsDigit(addedNumber[k]))
									{
										string numToReplace = "";
										for (int l = k; l >= 0; l--)
										{
											if (!Char.IsDigit(addedNumber[l]))
											{
												break;
											}
											numToReplace += addedNumber[l];
										}
										numToReplace = new string(numToReplace.Reverse().ToArray());
										int num = Convert.ToInt32(numToReplace);
										copy.Remove(k - numToReplace.Length + 1, numToReplace.Length);
										copy.Insert(k - numToReplace.Length + 1, (num + leftNumber).ToString());
										break;
									}
								}
								hasExploded = true;
								addedNumber = copy;
								break;
							}
						}
						else if (addedNumber[j] == ']')
						{
							bracketCount--;
						}
					}
					if (hasExploded)
					{
						continue;
					}
					bool hasSplit = false;
					for (int j = 0; j < addedNumber.Length; j++)
					{
						if (Char.IsDigit(addedNumber[j]))
						{
							string numberString = "";
							for (int k = j; k < addedNumber.Length; k++)
							{
								if (!char.IsDigit(addedNumber[k]))
								{
									break;
								}
								numberString += addedNumber[k];
							}
							int number;
							if (int.TryParse(numberString, out number))
							{
								if (number >= 10)
								{
									int a = (int)MathF.Floor(number / 2.0f);
									int b = (int)MathF.Ceiling(number / 2.0f);
									string replaceWith = "[" + a + "," + b + "]";
									addedNumber.Remove(j, numberString.Length);
									addedNumber.Insert(j, replaceWith);
									hasSplit = true;
									break;
								}
							}
						}
					}
					if (hasSplit)
					{
						continue;
					}
					break;
				}
				largest = Math.Max(largest, GetMagnitude(addedNumber.ToString()));
			}

			Console.WriteLine(largest);
		}

		private static int GetMagnitude(string number)
		{
			while (number.Contains("["))
			{
				StringBuilder newNumber = new StringBuilder(number);
				for (int i = number.Length - 1; i > 4; i--)
				{
					if (number[i] == ']')
					{
						string s = "";
						bool valid = true;
						for (int j = i - 1; j > 0; j--)
						{
							if (number[j] == ']')
							{
								valid = false;
								break;
							}
							if (number[j] == '[')
							{
								break;
							}
							s += number[j];
						}
						if (!valid)
						{
							continue;
						}
						s = new string(s.Reverse().ToArray());
						var split = s.Split(",");
						int sum = 3 * Convert.ToInt32(split[0]) + 2 * Convert.ToInt32(split[1]);
						newNumber.Remove(i - (s.Length + 1), s.Length + 2);
						newNumber.Insert(i - (s.Length + 1), sum);
					}
				}
				number = newNumber.ToString();
			}
			return Convert.ToInt32(number);
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
