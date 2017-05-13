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

        public void SetValue(int line, int column, double newValue)
        {
            if (line >= 0 && line < this.VSize && column >= 0 && column < this.HSize)
            {
                this.Values[line, column] = newValue;
                return;
            }
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
    }
}