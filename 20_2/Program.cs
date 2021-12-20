using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _20_2
{
	class Program
	{
		static int imageSize = 2000;
		static void Main(string[] args)
		{
			var input = ReadInput();
			string encoding = input[0];
			char[,] inputImage = new char[imageSize, imageSize];
			inputImage = GetEmptyImage();
			int startIndex = imageSize / 2;
			for (int y = 2; y < input.Count; y++)
			{
				for (int x = 0; x < input[y].Length; x++)
				{
					inputImage[startIndex + y, startIndex + x] = input[y][x];
				}
			}
			int litCount = 0;
			for (int k = 0; k < 50; k++)
			{
				litCount = 0;
				char[,] outputImage = new char[imageSize, imageSize];
				outputImage = GetEmptyImage();
				for (int y = 0; y < imageSize; y++)
				{
					for (int x = 0; x < imageSize; x++)
					{
						string code = "";
						for (int i = -1; i <= 1; i++)
						{
							for (int j = -1; j <= 1; j++)
							{
								if (((y + i) >= 0) && ((y + i) < imageSize) && ((x + j) >= 0) && ((x + j) < imageSize))
								{
									if (inputImage[y + i, x + j] == '#')
									{

										code += 1;
									}
									else if (inputImage[y + i, x + j] == '.')
									{

										code += 0;
									}
								}
							}
						}
						int lookupIndex = Convert.ToInt32(code, 2);
						outputImage[y, x] = encoding[lookupIndex];
						if (encoding[lookupIndex] == '#')
						{
							litCount++;
						}
					}
				}
				inputImage = outputImage;
			}
			Console.WriteLine(litCount);
		}

		private static void PrintImage(char[,] image)
		{
			for (int y = 0; y < imageSize; y++)
			{
				string line = "";
				for (int x = 0; x < imageSize; x++)
				{
					line += image[y, x];
				}
				Console.WriteLine(line);
			}
		}

		private static char[,] GetEmptyImage()
		{
			char[,] emptyImage = new char[imageSize, imageSize];
			for (int y = 0; y < imageSize; y++)
			{
				for (int x = 0; x < imageSize; x++)
				{
					emptyImage[y, x] = '.';
				}
			}
			return emptyImage;
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
