using System;
namespace DSolver
{
    public class Vector : Matrix
    {
		public int Size { get; private set; }

		public Vector() : base()
		{
			this.Size = this.VSize;
		}

		public Vector(double[] values) : base(values)
        {
            this.Size = this.VSize;
        }

        public Vector(int size) : base(size)
        {
            this.Size = size;
        }

		public Vector WithValues(double[] values)
		{
			base.WithValues(values);
			return this;
		}

		public Vector WithZeroes(int size)
		{
			base.WithZeroes(size, 1);
			this.Size = this.VSize;
			return this;
		}

        public Vector WithPolarValues(double r, double angle)
        {
            base.WithValues(new double[2] { r * Math.Cos(angle / 360 * 2 * Math.PI), r * Math.Sin(angle / 360 * 2 * Math.PI) });
            this.Size = this.VSize;
            return this;
        }

		public void SetValue(int index, double value)
		{
			if (index < 0 || index >= this.Size)
			{
				return;
			}
			this.SetValue(index, 0, value);
		}

        public double GetValue(int index)
        {
            if (index < 0 || index >= this.Size)
            {
                Console.WriteLine("Out of bounds");
                return 0;
            }
            return this.GetValue(index, 0);
        }

        public static Vector operator +(Vector m1, Vector m2)
        {
            if (m1.Size != m2.Size)
            {
                // TODO: throw error
            }
            Vector m = new Vector().WithZeroes(m1.Size);
            for (int i = 0; i < m1.VSize; i++)
            {
                m.SetValue(i, m1.GetValue(i) + m2.GetValue(i));
            }
            return m;
        }

        public static Vector operator -(Vector m1, Vector m2)
        {
            return m1 + (-1 * m2);
        }

        public static Vector operator *(double d, Vector m)
        {
            for (int i = 0; i < m.Size; i++)
            {
                    m.SetValue(i, d * m.GetValue(i));
            }
            return m;
        }

        public static Vector operator *(Vector m, double d)
        {
            return d * m;
        }
    }
}
