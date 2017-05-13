using System;

namespace DSolver
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			SquareMatrix matrix = new SquareMatrix(new double[2,2]{{1,2},{3,4}});
			matrix.Display();
			matrix.SetValue(1, 0, 78);
			matrix.Display();

			Vector vector = new Vector(new double[2]{1, 2});
			vector.Display();
			vector.SetValue(1, 687);
			vector.Display();
		}
	}
}
