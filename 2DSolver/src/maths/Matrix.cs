using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSolver
{
    public class Matrix
    {
        public int HSize { get; protected set; }

        public int VSize { get; protected set; }

        public double[,] Values { get; set; }

		public Matrix()
		{
		}

        public Matrix(double[,] values)
        {
            this.Values = values;
            this.VSize = values.GetLength(0);
            this.HSize = values.GetLength(1);
        }

        protected Matrix(double[] values)
        {
            this.VSize = values.GetLength(0);
            this.HSize = 1;
            this.Values = new double[this.VSize, this.HSize];
            int i;
            for (i = 0; i < this.VSize; i++)
            {
                this.SetValue(i, 0, values[i]);
            }
        }

        public Matrix(int lines, int columns)
        {
            int i, j;
            this.Values = new double[lines, columns];
            for (i = 0; i < lines; i++)
            {
                for (j = 0; j < columns; j++)
                {
                    this.SetValue(i, j, 0);
                }
            }
            this.HSize = columns;
            this.VSize = lines;
        }

        protected Matrix(int lines)
        {
            int i;
            this.Values = new double[lines, 1];
            for (i = 0; i < lines; i++)
            {
                this.SetValue(i, 0, 0);
            }
            this.HSize = 1;
            this.VSize = lines;
        }

		public virtual Matrix WithValues(double[,] values)
		{
			this.Values = values;
			this.VSize = values.GetLength(0);
			this.HSize = values.GetLength(1);
			return this;
		}

		protected virtual Matrix WithValues(double[] values)
		{
			this.VSize = values.GetLength(0);
			this.HSize = 1;
			this.Values = new double[this.VSize, this.HSize];
			int i;
			for (i = 0; i < this.VSize; i++)
			{
				this.SetValue(i, 0, values[i]);
			}
			return this;
		}

        public virtual Matrix WithSameValues(int lines, int columns, double value)
		{
            this.Values = new double[lines, columns];
            this.VSize = lines;
            this.HSize = columns;
			for (int i = 0; i < lines; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					this.SetValue(i, j, value);
				}
			}
			return this;
        }

        public virtual Matrix WithZeroes(int lines, int columns)
        {
            return this.WithSameValues(lines, columns, 0);
        }

        public virtual Matrix WithOnes(int lines, int columns)
        {
            return this.WithSameValues(lines, columns, 1);
        }

        public virtual Matrix WithVectors(Vector[] vectors)
        {
            this.VSize = vectors[0].Size;
            this.HSize = vectors.Length;
            this.Values = new double[this.VSize, this.HSize];

            for (int i = 0; i < vectors.Length; i++)
            {
                for (int j = 0; j < vectors[i].Size; j++)
                {
                    this.SetValue(j, i, vectors[i].GetValue(j));
                }
            }

            return this;
        }

        public void SetValue(int line, int column, double newValue)
        {
            if (line >= 0 && line < this.VSize && column >= 0 && column < this.HSize)
            {
                this.Values[line, column] = newValue;
                return;
            }
        }

        public void AddToValue(int line, int column, double valueToAdd)
        {
            this.SetValue(line, column, this.GetValue(line, column) + valueToAdd);
        }

        public double GetValue(int line, int column)
        {
            if (line < 0 || line >= this.VSize || column < 0 || column >= this.HSize)
            {
                Console.WriteLine("Out of bounds");
                return 0;
            }
            return this.Values[line, column];
        }

        public double[,] ToArray()
        {
            return this.Values;
        }

        public Vector[] toVectors()
        {
            Vector[] columns = new Vector[this.HSize];
            for (int j = 0; j < this.HSize; j++)
            {
                Vector vector = new Vector(this.VSize);
                for (int i = 0; i < this.HSize; i++)
                {
                    vector.SetValue(i, this.GetValue(i, j));
                }
                columns[j] = vector;
            }
            return columns;
        }

        public void Display(string name)
        {
            this.Display(name, new string[0]);
        }

        public void Display(string name, string[] unknownsNames)
        {
            int i, j;

            string spacer = "   ";
            if (name != "")
            {
                for (i = 0; i < name.Length; i++)
                {
                    spacer += " ";
                }
            }

            Console.WriteLine();
            for (i = 0; i < this.VSize; i++)
            {
                bool isMiddle = i == Math.Floor(Convert.ToDouble(this.VSize / 2));
                if (name !=  "") {
                    if (isMiddle)
                    {

                        Console.Write(" {0} =", name);
                    }
                    else
                    {
                        Console.Write(spacer);
                    }
                }


                if (this.HSize == 1 && unknownsNames.Length > 0)
                {
                    string space = "   ";
                    if (isMiddle)
                    {
                        space = " = ";
                    }
                    Console.Write("| {0} |{1}", unknownsNames[i], space);
                }

                Console.Write(" |");
                for (j = 0; j < this.HSize; j++)
                {
                    Console.Write("{0,8:f3} ", this.GetValue(i, j));
                }
                Console.Write("| \n");
            }
            Console.WriteLine();
        }

        public void Display()
        {
            Display("");
        }

        public float Determinant()
        {
            float determinent = 0;

            if (this.VSize != this.HSize)
                throw new Exception("Attempt to find the determinent of a non square matrix");
            //return 0;

            // Get the determinent of a 2x2 matrix
            if (this.VSize == 2 && this.HSize == 2)
            {
                determinent = (float) ((this.Values[0, 0] * this.Values[1, 1]) - (this.Values[0, 1] * this.Values[1, 0]));
                return determinent;
            }

            Matrix tempMtx = new Matrix().WithZeroes(this.VSize - 1, this.HSize - 1);

            // Find the determinent with respect to the first row
            for (int j = 0; j < this.HSize; j++)
            {
                tempMtx = this.Minor(0, j);

                // Recursively add the determinents
                determinent += (float) ((int)Math.Pow(-1, j) * this.Values[0, j] * tempMtx.Determinant());

            }

            return determinent;
        }

        public Matrix Minor(int row, int column)
        {
            if (this.VSize < 2 || this.HSize < 2)
                throw new Exception("Minor not available");

            int i, j = 0;

            Matrix minorMtx = new Matrix(this.VSize - 1, this.HSize - 1);

            // Find the minor with respect to the first element
            for (int k = 0; k < minorMtx.VSize; k++)
            {

                if (k >= row)
                    i = k + 1;
                else
                    i = k;

                for (int l = 0; l < minorMtx.HSize; l++)
                {
                    if (l >= column)
                    {
                        j = l + 1;
                    }
                    else
                    {
                        j = l;
                    }

                    minorMtx.Values[k, l] = this.Values[i, j];
                }
            }

            return minorMtx;
        }


        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.HSize != m2.HSize || m2.VSize != m2.VSize)
            {
                // TODO: throw error
            }
            Matrix m = new Matrix().WithZeroes(m1.VSize, m1.HSize);
            for (int i = 0; i < m1.VSize; i++)
            {
                for (int j = 0; j < m1.HSize; j++)
                {
                    m.SetValue(i, j, m1.GetValue(i, j) + m2.GetValue(i, j));
                }
            }
            return m;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            return m1 + (-1 * m2);
        }

        public static Matrix operator *(double d, Matrix m)
        {
            for (int i = 0; i < m.VSize; i++)
            {
                for (int j = 0; j < m.HSize; j++) {
                    m.SetValue(i, j, d * m.GetValue(i, j));
                }
            }
            return m;
        }

        public static Matrix operator *(Matrix m, double d)
        {
            return d * m;
        }

        public double this[int i, int j]
        {
            get { return this.Values[i, j]; }
            set { this.Values[i, j] = value; }
        }
    }
}
