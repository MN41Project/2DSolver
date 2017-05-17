using System;

namespace DSolver
{
	public class BoundaryCondition
	{

		public bool HasOnX { get; private set; }
		public bool HasOnY { get; private set; }

		public double XValue { get; private set; }
		public double YValue { get; private set; }

		public BoundaryCondition ()
		{
			this.HasOnX = false;
			this.HasOnY = false;
		}

		public BoundaryCondition WithX(double x)
		{
			this.HasOnX = true;
			this.XValue = x;
			return this;
		}

		public BoundaryCondition WithY(double y)
		{
			this.HasOnY = true;
			this.YValue = y;
			return this;
		}
	}
}

