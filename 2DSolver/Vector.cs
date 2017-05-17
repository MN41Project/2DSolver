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
                return 0;
            }
            return this.GetValue(index, 0);
        }
    }
}
