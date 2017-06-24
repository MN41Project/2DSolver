using System;
namespace DSolver
{
    public class LinearSystem
    {
        public const int GAUSS_METHOD = 0;
        public const int LU_METHOD = 1;
        public const int THOMAS_METHOD = 2;

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

        public Vector Solve(int method, bool showDetails)
        {
            switch (method)
            {
                case LinearSystem.GAUSS_METHOD:
                    return this.SolveWithGauss(showDetails);
                case LinearSystem.LU_METHOD:
                    return this.SolveWithLU(showDetails);
                case LinearSystem.THOMAS_METHOD:
                    return this.SolveWithThomas(showDetails);
                default:
                    throw new Exception("This method doesn't exist");
            }
        }

        public Vector Solve(bool showDetails)
        {
            return this.Solve(LinearSystem.GAUSS_METHOD, showDetails);
        }

        public Vector Solve()
        {
            return this.Solve(LinearSystem.GAUSS_METHOD, false);
        }

        private Vector SolveWithGauss(bool showDelaits)
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

        private Vector SolveWithLU(bool showDetails)
        {
            int n = this.Size;
            this.Solution = new Vector(n);
            Matrix L = new Matrix().WithZeroes(n, n);
            Matrix U = new Matrix().WithZeroes(n, n);
            Vector Y = new Vector().WithZeroes(n);

            // Computing L
            for (var i = 0; i < n; i++) {
                L.SetValue(i, i, 1);
            }
            for (int r = 0; r < n; r++) {
                for (int j = r; j < n; j++) {
                    double sum = 0;
                    for (int k = 0; k < r; k++) {
                        sum += L.GetValue(r, k) * U.GetValue(k, j);
                    }
                    U.SetValue(r, j, this.K.GetValue(r, j) - sum);
                }
                for (int i = r; i < n; i++) {
                    double sum = 0;
                    for (int k = 0; k < r; k++) {
                        sum += L.GetValue(i, k) * U.GetValue(k, r);
                    }
                    L.SetValue(i, r, (this.K.GetValue(i, r) - sum) / U.GetValue(r, r));
                }
            }

            // Compute Y=U.X
            Y.SetValue(0, this.F.GetValue(0));
            for (int k = 1; k < n; k++) {
                double sum = 0;
                for (int r = 0; r < k; r++) {
                    sum += L.GetValue(k, r) * Y.GetValue(r);
                }
                Y.SetValue(k, this.F.GetValue(k) - sum);
            }

            if (showDetails)
            {
                L.Display("L");
                U.Display("U");
                Y.Display("Y");
            }

            // Compute X solving UX=Y
            this.Solution.SetValue(n - 1, Y.GetValue(n - 1) / U.GetValue(n - 1, n - 1));
            this.Solution.SetValue(n - 2, (Y.GetValue(n - 2) - U.GetValue(n - 2, n-1) * this.Solution.GetValue(n - 1)) / U.GetValue(n - 2, n - 2));
            for (int k = n - 2; k >= 0; k--) {
                double sum = 0;
                for (int r = k + 1; r < n; r++) {
                    sum += U.GetValue(k, r) * this.Solution.GetValue(r);
                }
                this.Solution.SetValue(k, (Y.GetValue(k) - sum) / U.GetValue(k, k));
            }

            return this.Solution;
        }

        private Vector SolveWithThomas(bool showDetails)
        {
            return this.Solution;
        }

        public void DisplaySolution()
        {
            this.Solution.Display();
        }

    }
}
