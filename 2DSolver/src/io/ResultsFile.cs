using System;
using System.IO;

namespace DSolver
{
    public class ResultsFile
    {
        public string Path { get; private set; }
        public DiscreteSystem System { get; private set; }

        public ResultsFile()
        {
        }

        public ResultsFile WithPath(string path)
        {
            this.Path = path;
            return this;
        }

        public ResultsFile WithDiscreteSystem(DiscreteSystem sys)
        {
            this.System = sys;
            return this;
        }

        private void DisplayLinearSystem(StreamWriter file, LinearSystem sys, string[] unknownsNames)
        {
            for (int i = 0; i < sys.Size; i++)
            {
                file.Write("|");
                for (int j = 0; j < sys.Size; j++)
                {
                    file.Write("{0,8:f4} ", sys.K.GetValue(i, j));
                }
                file.Write("|");
                if (i == Math.Floor(Convert.ToDouble(sys.Size / 2)))
                {
                    file.Write(" . ");
                }
                else
                {
                    file.Write("   ");
                }
                file.Write("|");
                if (unknownsNames.Length == sys.Size)
                {
                    file.Write(" {0} ", unknownsNames[i]);
                }
                else
                {
                    file.Write(" x{0} ", i);
                }
                file.Write("|");
                if (i == Math.Floor(Convert.ToDouble(sys.Size / 2)))
                {
                    file.Write(" = ");
                }
                else
                {
                    file.Write("   ");
                }

                file.Write("|");
                file.Write("{0,8:f4} ", sys.F.GetValue(i));
                file.Write("|\n");
            }
        }

        private void DisplayMatrix(StreamWriter file, Matrix matrix, string name, string[] unknownsNames)
        {
            string spacer = "   ";
            if (name != "")
            {
                for (int i = 0; i < name.Length; i++)
                {
                    spacer += " ";
                }
            }

            file.WriteLine();
            for (int i = 0; i < matrix.VSize; i++)
            {
                bool isMiddle = i == Math.Floor(Convert.ToDouble(matrix.VSize / 2));
                if (name !=  "") {
                    if (isMiddle)
                    {

                        file.Write(" {0} =", name);
                    }
                    else
                    {
                        file.Write(spacer);
                    }
                }


                if (matrix.HSize == 1 && unknownsNames.Length > 0)
                {
                    string space = "   ";
                    if (isMiddle)
                    {
                        space = " = ";
                    }
                    file.Write("| {0} |{1}", unknownsNames[i], space);
                }

                file.Write(" |");
                for (int j = 0; j < matrix.HSize; j++)
                {
                    file.Write("{0,8:f3} ", matrix[i, j]);
                }
                file.Write("| \n");
            }
            file.WriteLine();
        }

        public void Write(bool showDetails)
        {
            
            StreamWriter file = new StreamWriter(this.Path);
            if (showDetails)
            {
                this.DisplayMatrix(file, this.System.AssembledMatrix, "Assembled matrix", new string[0]);
                this.DisplayMatrix(file, this.System.SecondMember, "Second member (zeroes instead of reactions)", new string[0]);
                file.WriteLine("\nBC-simplified system\n");
                this.DisplayLinearSystem(file, this.System.SimpleSystem, this.System.SimpleSystem.UnknownsNames);
            }
            file.WriteLine("\nResults");
            this.DisplayMatrix(file, this.System.SimpleSystem.Solution, "", this.System.SimpleSystem.UnknownsNames);
            file.Close();
        }
    }
}

