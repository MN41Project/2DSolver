using System;
namespace DSolver
{
    public class SquareMatrix : Matrix 
    {
		public int Size { get; private set; }

		public SquareMatrix() : base()
		{
		}

        public SquareMatrix(double[,] values) : base(values)
        {
            if (this.VSize != this.HSize)
            {
                return;
            }
            this.Size = this.VSize;
        }

        public SquareMatrix(int size) : base(size, size)
        {
            this.Size = size;
        }

		public SquareMatrix WithValues(double[,] values)
		{
			base.WithValues(values);
			this.Size = this.HSize;
			return this;
		}

		public SquareMatrix WithZeroes(int size)
		{
			this.WithZeroes(size, size);
			this.Size = this.HSize;
			return this;
		}
    }
}
