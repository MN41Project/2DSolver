using System;

namespace DSolver
{
	public class Node
	{
		public int Id { get; private set; }
		public double PosX { get; private set; }
		public double PosY { get; private set; }
		public Vector Force { get; private set; }

		public Node ()
		{
		}

		public Node WithId(int id) {
			this.Id = id;
			return this;
		}

		public Node WithPosX(double x) {
			this.PosX = x;
			return this;
		}

		public Node WithPosY(double y) {
			this.PosY = y;
			return this;
		}

		public Node WithForce(Vector f){
			this.Force = f;
			return this;
		}
	}
}

