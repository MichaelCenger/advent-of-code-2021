using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _19_2
{
	class Program
	{
		static List<(int x, int y, int z)> scannerPositions = new List<(int x, int y, int z)>();
		static void Main(string[] args)
		{
			var input = ReadInput();
			var scannerInput = input.Split("\r\n\r\n").ToList();
			List<HashSet<(int x, int y, int z)>> placedScanners = new List<HashSet<(int x, int y, int z)>>();
			placedScanners.Add(GetAllBeaconsFromScannerInput(scannerInput[0]));
			scannerInput.RemoveAt(0);
			scannerPositions.Add((0, 0, 0));
			while (scannerInput.Count != 0)
			{
				for (int i = 0; i < scannerInput.Count; i++)
				{
					string scannerString = scannerInput[i];
					HashSet<(int x, int y, int z)> scannerBeacons = GetAllBeaconsFromScannerInput(scannerString);
					List<HashSet<(int x, int y, int z)>> rotatedBeacons = GetRotatedBeacons(scannerBeacons);
					if (TryPlaceScanner(rotatedBeacons, placedScanners))
					{
						scannerInput.RemoveAt(i);
						break;
					}
				}
			}
			HashSet<(int x, int y, int z)> totalBeacons = new HashSet<(int x, int y, int z)>();
			foreach (var beacons in placedScanners)
			{
				totalBeacons.UnionWith(beacons);
			}

			int maxDistance = 0;
			foreach (var scanner1 in scannerPositions)
			{
				foreach (var scanner2 in scannerPositions)
				{
					maxDistance = Math.Max(Math.Abs(scanner1.x - scanner2.x) + Math.Abs(scanner1.y - scanner2.y) + Math.Abs(scanner1.z - scanner2.z), maxDistance);
				}
			}
			Console.WriteLine(maxDistance);
		}

		private static bool TryPlaceScanner(List<HashSet<(int x, int y, int z)>> newRotatedScanners, List<HashSet<(int x, int y, int z)>> existingScanners)
		{
			//For every placed scanner
			foreach (var scanner in existingScanners)
			{
				//For every beacon of that scanner
				foreach (var existingBeacon in scanner)
				{
					//For evey rotation of the new scanner
					foreach (var newRotatedScanner in newRotatedScanners)
					{
						foreach (var newBeacon in newRotatedScanner)
						{
							(int x, int y, int z) newScannerPosition = (existingBeacon.x - newBeacon.x, existingBeacon.y - newBeacon.y, existingBeacon.z - newBeacon.z);
							HashSet<(int x, int y, int z)> transformedBeacons = GetTransformedBeacons(newRotatedScanner, newScannerPosition);
							HashSet<(int x, int y, int z)> intersectionSet = new HashSet<(int x, int y, int z)>(scanner);
							intersectionSet.IntersectWith(transformedBeacons);
							if (intersectionSet.Count >= 3)
							{
								existingScanners.Add(transformedBeacons);
								scannerPositions.Add(newScannerPosition);
								return true;
							}
						}
					}
				}
			}

			return false;
		}

		private static HashSet<(int x, int y, int z)> GetAllBeaconsFromScannerInput(string scannerInput)
		{
			HashSet<(int x, int y, int z)> beacons = new HashSet<(int x, int y, int z)>();
			var lines = scannerInput.Split("\r\n");
			for (int i = 1; i < lines.Length; i++)
			{
				var coords = lines[i].Split(",");
				beacons.Add((Convert.ToInt32(coords[0]), Convert.ToInt32(coords[1]), Convert.ToInt32(coords[2])));
			}
			return beacons;
		}

		private static List<HashSet<(int x, int y, int z)>> GetRotatedBeacons(HashSet<(int x, int y, int z)> beacons)
		{
			List<HashSet<(int x, int y, int z)>> rotatedBeacons = new List<HashSet<(int x, int y, int z)>>();
			List<(int x, int y, int z)> signs =
				new List<(int x, int y, int z)>() {
					(1, 1, 1),
					(-1, 1, 1),
					(1, -1, 1),
					(1, 1, -1),
					(-1, -1, 1),
					(1, -1, -1),
					(-1, 1, -1),
					(-1, -1, -1)
				};
			for (int i = 0; i < 6; i++)
			{
				HashSet<(int x, int y, int z)> shifted = new HashSet<(int x, int y, int z)>();
				if (i == 0)
				{
					foreach (var beacon in beacons)
					{
						shifted.Add((beacon.x, beacon.z, beacon.y));
					}
				}
				else if (i == 1)
				{
					foreach (var beacon in beacons)
					{
						shifted.Add((beacon.z, beacon.y, beacon.x));
					}
				}
				else if (i == 2)
				{
					foreach (var beacon in beacons)
					{
						shifted.Add((beacon.y, beacon.x, beacon.z));
					}
				}
				else if (i == 3)
				{
					foreach (var beacon in beacons)
					{
						shifted.Add((beacon.x, beacon.y, beacon.z));
					}
				}
				else if (i == 4)
				{
					foreach (var beacon in beacons)
					{
						shifted.Add((beacon.z, beacon.x, beacon.y));
					}
				}
				else if (i == 5)
				{
					foreach (var beacon in beacons)
					{
						shifted.Add((beacon.y, beacon.z, beacon.x));
					}
				}
				foreach (var sign in signs)
				{
					HashSet<(int x, int y, int z)> rotated = new HashSet<(int x, int y, int z)>();
					foreach (var beacon in shifted)
					{
						rotated.Add((sign.x * beacon.x, sign.y * beacon.y, sign.z * beacon.z));
					}
					rotatedBeacons.Add(rotated);
				}
			}
			return rotatedBeacons;
		}

		private static HashSet<(int x, int y, int z)> GetTransformedBeacons(HashSet<(int x, int y, int z)> beacons, (int x, int y, int z) origin)
		{
			HashSet<(int x, int y, int z)> transformedBeacons = new HashSet<(int x, int y, int z)>();
			foreach (var pos in beacons)
			{
				transformedBeacons.Add((pos.x + origin.x, pos.y + origin.y, pos.z + origin.z));
			}
			return transformedBeacons;
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
