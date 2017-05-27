using System;
namespace DSolver
{
    public class LinearSystem
    {
		public SquareMatrix K { get; private set; }
        public Vector F { get; private set; }
        public Vector Solution { get; private set; }
        public int Size { get; private set; }

		public LinearSystem(SquareMatrix K, Vector F)
		{
			if (K.Size != F.Size)
            {
                return;
            }
			this.K = K;
			this.F = F;
            this.Size = K.Size;
		}

        public void Display()
        {
            int i, j;
            for (i = 0; i < this.Size; i++)
            {
                Console.Write("|");
                for (j = 0; j < this.Size; j++)
                {
                    Console.Write("{0,8:f4} ", this.K.GetValue(i, j));
                }
                Console.Write("|");
                if (i == Math.Floor(Convert.ToDouble(this.Size / 2)))
                {
                    Console.Write(" . ");
                }
                else
                {
                    Console.Write("   ");
                }
                Console.Write("|");
                Console.Write(" u{0} ", i);
                Console.Write("|");
                if (i == Math.Floor(Convert.ToDouble(this.Size / 2)))
                {
                    Console.Write(" = ");
                }
                else
                {
                    Console.Write("   ");
                }

                Console.Write("|");
                Console.Write("{0,8:f4} ", this.F.GetValue(i));
                Console.Write("|\n");
            }
        }

        public Vector Solve()
        {
            int i, j, k;
            double coeff;
            Matrix matrix = new Matrix(this.Size, this.Size + 1);
            for (i = 0; i < matrix.VSize; i++)
            {
                for (j = 0; j < matrix.HSize - 1; j++)
                {
                    matrix.SetValue(i, j, this.K.GetValue(i, j));
                }
                matrix.SetValue(i, matrix.HSize - 1, this.F.GetValue(i));
            }

            for (k = 0; k < this.Size - 1; k++)
            {
                for (i = k + 1; i < this.Size; i++)
                {
                    coeff = matrix.GetValue(i, k) / matrix.GetValue(k, k);
                    for (j = k; j < this.Size + 1; j++)
                    {
                        matrix.SetValue(i, j, matrix.GetValue(i, j) - matrix.GetValue(k, j) * coeff);
                    }
                }
            }
            
            double sum;
            this.Solution = new Vector(this.Size);
            this.Solution.SetValue(matrix.VSize - 1, matrix.GetValue(matrix.VSize - 1, matrix.HSize - 1));

            for (i = this.Size - 1; i >= 0; i--)
            {
                sum = 0;
                for (j = i + 1; j < this.Size; j++)
                {
                    sum += matrix.GetValue(i, j) * this.Solution.GetValue(j);
                }
                this.Solution.SetValue(i, (matrix.GetValue(i, this.Size) - sum) / matrix.GetValue(i, i));
            }

            return this.Solution;
        }

        public void DisplaySolution()
        {
            this.Solution.Display();
        }

    }
}
