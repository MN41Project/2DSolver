using System;
using System.Collections.Generic;
using System.Linq;

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

        public SquareMatrix WithVectors(Vector[] vectors)
        {
            base.WithVectors(vectors);
            this.Size = this.HSize;
            return this;
        }

        public bool IsTridiagonal()
        {
            if (this.Size < 3)
            {
                return false;
            }

            List<string> tridiagonalValues = new List<string>();

            for (var i = 0; i < this.Size; i++)
            {
                for (var j = 0; j < this.Size; j++)
                {
                    if (j > i + 1)
                    {
                        tridiagonalValues.Add(i + "," + j);
                    }
                }
            }

            for (var i = 0; i < this.Size; i++)
            {
                for (var j = 0; j < this.Size; j++)
                {
                    if (j < i - 1)
                    {
                        tridiagonalValues.Add(i + "," + j);
                    }
                }
            }

            foreach (string ij in tridiagonalValues)
            {
                Console.WriteLine(ij);
            }
            Console.ReadKey();

            bool zeroesAreCorrect = true;

            for (var i = 0; i < this.Size; i++)
            {
                for (var j = 0; j < this.Size; j++)
                {
                    string val = i + "," + j;
                    if (tridiagonalValues.Exists(x => x.Equals(val)))
                    {
                        if (this[i, j] != 0)
                        {
                            zeroesAreCorrect = false;
                        }
                    }
                }
            }

            return zeroesAreCorrect;
        }
    }
}
