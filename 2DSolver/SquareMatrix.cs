using System;
namespace DSolver
{
    public class SquareMatrix
    {
		public int Size { get; private set; }

		private double[,] Values { get; set; }

		public SquareMatrix(double[,] values)
		{
			this.Values = values;
			this.Size = values.GetLength(0);
		}

		public void SetValue(int line, int column, double newValue)
		{
			if (line >= 0 && line < this.Size && column >= 0 && column < this.Size)
			{
				this.Values[line, column] = newValue;
				return;
			}
		}

		public void Display()
		{
			int i, j;
			string name = "Mat";

			Console.Write("{0} = [\n", name);
			for (i = 0; i < this.Size; i++)
			{
				for (j = 0; j < this.Size - 1; j++)
				{
					Console.Write("{0,8:f4} ", this.Values[i, j]);
				}
				Console.Write("{0, 8:f4}  \n", this.Values[i, this.Size - 1]);
			}
			Console.Write("  ]\n\n", this.Values[i - 1, this.Size - 1]);
		}
    }
}
