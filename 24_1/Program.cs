using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _24_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var instructions = ReadInput();
			Dictionary<char, int> register = new Dictionary<char, int>();
			Dictionary<(int instructionBlock, int w, int z), (int x, int y, int z, int w)> mem = new Dictionary<(int instructionBlock, int w, int z), (int x, int y, int z, int w)>();
			register.Add('x', 0);
			register.Add('y', 0);
			register.Add('z', 0);
			register.Add('w', 0);


			register['x'] = 0;
			register['y'] = 0;
			register['z'] = 0;
			register['w'] = 0;

			List<string> instructionBlock = new List<string>();
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

			for (int i = 1; i <= 9; i++)
			{
				for (int j = 1; j <= 9; j++)
				{
					for (int k = 1; k <= 9; k++)
					{
						for (int l = 1; l <= 9; l++)
						{
							for (int m = 1; m <= 9; m++)
							{
								for (int n = 1; n <= 9; n++)
								{
									for (int o = 1; o <= 9; o++)
									{
										for (int p = 1; p <= 9; p++)
										{
											for (int q = 1; q <= 9; q++)
											{
												for (int r = 1; r <= 9; r++)
												{
													for (int s = 1; s <= 9; s++)
													{
														int currentBlock = 0;
														List<int> inputVariables = new List<int>() { 9, 9, 9 };
														inputVariables.Add(i);
														inputVariables.Add(j);
														inputVariables.Add(k);
														inputVariables.Add(l);
														inputVariables.Add(m);
														inputVariables.Add(n);
														inputVariables.Add(o);
														inputVariables.Add(p);
														inputVariables.Add(q);
														inputVariables.Add(r);
														inputVariables.Add(s);

														foreach (var insBlock in instructionBlock)
														{
															int inputNumber = inputVariables[currentBlock];

															(int instructionBlock, int w, int z) state;
															state.instructionBlock = currentBlock;
															state.w = inputNumber;
															state.z = register['z'];
															if (state.z > 1000000)
															{
																break;
															}
															if (mem.ContainsKey(state))
															{
																var res = mem[state];
																register['x'] = res.x;
																register['y'] = res.y;
																register['z'] = res.z;
																register['w'] = res.w;
																continue;
															}
															foreach (var line in insBlock.Split(","))
															{
																string[] instruction = line.Split(" ");
																string op = instruction[0];
																char location = instruction[1][0];
																switch (op)
																{
																	case "inp":
																		register[location] = inputNumber;
																		currentBlock++;
																		break;
																	case "add":
																		string bInstruction = instruction[2];
																		int b = 0;
																		if (int.TryParse(bInstruction, out b))
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
																		if (int.TryParse(bInstruction, out b))
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
																		if (int.TryParse(bInstruction, out b))
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
																		if (int.TryParse(bInstruction, out b))
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
																		if (int.TryParse(bInstruction, out b))
																		{

																		}
																		else
																		{
																			b = register[bInstruction[0]];
																		}
																		register[location] = Convert.ToInt32(register[location] == b);
																		break;
																}
															}
															mem.Add(state, (register['x'], register['y'], register['z'], register['w']));
														}
														if (register['z'] == 0)
														{
															Console.WriteLine("FOUND");
															foreach (var num in inputVariables)
															{
																Console.WriteLine(num);
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

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
