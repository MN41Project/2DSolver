using System;
namespace DSolver
{
    public class SquareMatrix : Matrix 
    {
		public int Size { get; private set; }

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
    }
}
