using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _22_2
{
	class Cuboid
	{
		public List<Cuboid> Off = new List<Cuboid>();
		long xMin, xMax, yMin, yMax, zMin, zMax;
		public Cuboid()
		{

		}

		public Cuboid(long xMin, long xMax, long yMin, long yMax, long zMin, long zMax)
		{
			this.xMin = xMin;
			this.xMax = xMax;
			this.yMin = yMin;
			this.yMax = yMax;
			this.zMin = zMin;
			this.zMax = zMax;
		}

		public long GetOnCount()
		{
			long offCount = 0;
			foreach (var off in Off)
			{
				offCount += off.GetOnCount();
			}
			return (Math.Abs(xMax - xMin) + 1) * (Math.Abs(yMax - yMin) + 1) * (Math.Abs(zMax - zMin) + 1) - offCount;
		}

		public bool DoesIntersectWith(Cuboid other)
		{
			return other.xMax >= xMin && other.xMin <= xMax &&
				other.yMax >= yMin && other.yMin <= yMax &&
				other.zMax >= zMin && other.zMin <= zMax;
		}

		public Cuboid IntersectWith(Cuboid other)
		{
			Cuboid intersection = new Cuboid();
			intersection.xMax = Math.Min(xMax, other.xMax);
			intersection.yMax = Math.Min(yMax, other.yMax);
			intersection.zMax = Math.Min(zMax, other.zMax);
			intersection.xMin = Math.Max(xMin, other.xMin);
			intersection.yMin = Math.Max(yMin, other.yMin);
			intersection.zMin = Math.Max(zMin, other.zMin);
			return intersection;
		}

		public void TurnOff(Cuboid off)
		{
			foreach (var o in Off)
			{
				if (off.DoesIntersectWith(o))
				{
					Cuboid intersection = off.IntersectWith(o);
					o.TurnOff(intersection);
				}
			}
			Off.Add(off);
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			var input = ReadInput();
			List<Cuboid> on = new List<Cuboid>();

			foreach (var line in input)
			{
				bool turnOn = line.Split(" ")[0] == "on";
				var coords = line.Split(" ")[1].Split(",");
				int xMin = Convert.ToInt32(coords[0].Split("..")[0]);
				int xMax = Convert.ToInt32(coords[0].Split("..")[1]);
				int yMin = Convert.ToInt32(coords[1].Split("..")[0]);
				int yMax = Convert.ToInt32(coords[1].Split("..")[1]);
				int zMin = Convert.ToInt32(coords[2].Split("..")[0]);
				int zMax = Convert.ToInt32(coords[2].Split("..")[1]);

				Cuboid newCuboid = new Cuboid(xMin, xMax, yMin, yMax, zMin, zMax);
				foreach (var alreadyOn in on)
				{
					if (newCuboid.DoesIntersectWith(alreadyOn))
					{
						alreadyOn.TurnOff(newCuboid.IntersectWith(alreadyOn));
					}
				}
				if (turnOn)
				{
					on.Add(newCuboid);
				}
				long count = 0;
				foreach (var c in on)
				{
					count += c.GetOnCount();
				}
				Console.WriteLine(count);
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
