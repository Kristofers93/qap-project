using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structures
{
    public class Matrix2D
    {
        private float[,] m;
        public Matrix2D(int n)
        {
            m = new float[n,n];
        }
        public float this[int i, int j]
        {
            get { return m[i, j]; }
            set { m[i, j] = value; }
        }
    }
}
