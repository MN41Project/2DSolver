using System;

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
	}
}

