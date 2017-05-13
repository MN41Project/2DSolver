using System;
namespace DSolver
{
    public class Vector
    {
		public int Size { get; private set; }

		private double[] Values { get; set; }

		public Vector(double[] values)
		{
			this.Values = values;
			this.Size = values.GetLength(0);
		}

		public void Display()
		{
			int i;
			string name = "Vec";
			Console.Write("{0} = [", name);
			for (i = 0; i < this.Size - 1; i++)
			{
				Console.Write("{0,8:f4} ", this.Values[i]);
			}
			Console.Write("{0,8:f4}  ]\n\n", this.Values[this.Size - 1]);
		}

		public void SetValue(int index, double value)
		{
			if (index < 0 || index >= this.Size)
			{
				return;
			}
			this.Values[index] = value;
		}
    }
}
