using System;
using System.IO;

namespace DSolver
{
	public abstract class Utils
	{
		public Utils ()
		{
		}

		public static double RadToDeg(double rad)
		{
			return rad * 180 / Math.PI;
        }

        public static double[] LineToDoubles(string line)
        {
            string[] parts = line.Split(' ');
            if (parts.Length == 0) {
                return new double[0];
            }
            double[] doubles = new double[parts.Length];
            for (var i = 0; i < parts.Length; i++)
            {
                parts[i].Replace(',', '.');
                doubles[i] = Convert.ToDouble(parts[i]);
            }
            return doubles;
        }

        public static int[] LineToInts(string line)
        {
            double[] doubles = LineToDoubles(line);
            int[] ints = new int[doubles.Length];
            for (var i = 0; i < doubles.Length; i++)
            {
                ints[i] = Convert.ToInt32(doubles[i]);
            }
            return ints;
        }

        public static void Pause()
        {
            Console.WriteLine("\n\nHit ENTER to continue");
            Console.ReadLine();
        }
	}
}

