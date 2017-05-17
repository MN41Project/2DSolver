using System;

namespace DSolver
{
	public class Element
	{
		public const int ELASTIC_BEAM = 0;

		public int Type { get; private set; }
		public Node FirstNode { get; private set; }
		public Node SecondNode { get; private set; }
		public Matrix ElementaryMatrix { get; private set; }
		public double Length { get; private set; }
		public double Angle { get; private set; }
		public double YoungModulus { get; private set; }
		public double Section { get; private set; }

		public Element()
		{
		}

		public Element(int type, Node first, Node second)
		{
			this.Type = type;
			this.FirstNode = first;
			this.SecondNode = second;

		}

		public Element WithType(int t)
		{
			this.Type = t;
			return this;
		}

		public Element WithFirstNode(Node n)
		{
			this.FirstNode = n;
			return this;
		}

		public Element WithYoungModulus(double y)
		{
			this.YoungModulus = y;
			return this;
		}

		public Element WithSection(double s)
		{
			this.Section = s;

			// TODO: check if both nodes are set
			this.fillProperties();
			return this;
		}

		public Element WithSecondNode(Node n)
		{
			this.SecondNode = n;
			return this;
		}

		private void fillProperties()
		{
			double xLength = this.SecondNode.PosX - this.FirstNode.PosX;
			double yLength = this.SecondNode.PosY - this.FirstNode.PosY;

			this.Length = Math.Sqrt(xLength * xLength + yLength * yLength);
			this.Angle = Math.Atan2(yLength, xLength);

			SquareMatrix matrix = new SquareMatrix().WithValues(new double[4, 4]);
			SquareMatrix subMatrix = this.GetSubMatrix();
			double coeff = this.GetCoeff();

			for (int i = 0; i < 4; i++) {
				for (int j = 0; j < 4; j++) {
					if (i < 2 && j < 2) {
						matrix.SetValue (i, j, coeff * subMatrix.GetValue (i, j));
					} else if (i >= 2 && j >= 2) {
						matrix.SetValue (i, j, coeff * subMatrix.GetValue (i - 2, j - 2));
					} else if (i < 2) {
						matrix.SetValue(i, j, -coeff * subMatrix.GetValue(i, j - 2));
					} else if (j< 2) {
						matrix.SetValue(i, j, -coeff * subMatrix.GetValue(i - 2, j));
					}
				}
			}

			this.ElementaryMatrix = matrix;
		}

		private double GetCoeff()
		{
			switch (this.Type) {
				case Element.ELASTIC_BEAM:
				default:
					return this.YoungModulus * this.Section / this.Length;
			}
		}

		private SquareMatrix GetSubMatrix()
		{
			switch (this.Type) {
				case Element.ELASTIC_BEAM:
				default:
					return new SquareMatrix().WithValues(new double[2,2]{
						{ Math.Pow(Math.Cos(this.Angle), 2), Math.Cos(this.Angle)*Math.Sin(this.Angle) },
						{ Math.Cos(this.Angle)*Math.Sin(this.Angle), Math.Pow(Math.Sin(this.Angle), 2) }
					});
			}	
		}
	}
}

