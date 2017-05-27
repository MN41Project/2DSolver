﻿using System;
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

        protected double[,] Values { get; set; }

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

		public virtual Matrix WithZeroes(int lines, int columns)
		{
			this.Values = new double[lines, columns];
			for (int i=0; i<lines; i++)
			{
				for (int j=0; j<columns; j++)
				{
					this.SetValue(i, j, 0);
				}
			}
			this.VSize = lines;
			this.HSize = columns;
			return this;
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
                if (name !=  "") {
                    if (i == Math.Floor(Convert.ToDouble(this.VSize / 2)))
                    {

                        Console.Write(" {0} =", name);
                    }
                    else
                    {
                        Console.Write(spacer);
                    }
                }
                Console.Write(" |");
                for (j = 0; j < this.HSize; j++)
                {
                    Console.Write("{0,8:f4} ", this.GetValue(i, j));
                }
                Console.Write("| \n");
            }
            Console.WriteLine();
        }

        public void Display()
        {
            Display("");
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.HSize != m2.HSize || m2.VSize != m2.VSize)
            {
                // TODO: throw error
            }
            Matrix m = new Matrix().WithZeroes(m1.VSize, m2.HSize);
            for (int i = 0; i < m1.VSize; i++)
            {
                for (int j = 0; j < m1.HSize; j++)
                {
                    m.SetValue(i, j, m1.GetValue(i, j) + m2.GetValue(i, j));
                }
            }
            return m;
        }
    }
}
