using System;
namespace DSolver
{
    public class LinearSystem
    {
		public SquareMatrix K { get; private set; }
		public Vector U { get; private set; }
		public int Size { get; private set; }

		public LinearSystem(SquareMatrix K, Vector U)
		{
			if (K.Size == U.Size)
				this.K = K;
			this.U = U;
		}
    }
}
