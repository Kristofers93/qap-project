using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structures
{
    public class IntMatrix2D
    {
        private int[,] m;
        private int n;
        public IntMatrix2D(int n)
        {
            m = new int[n,n];
            this.n = n;
        }
        public int this[int i, int j]
        {
            get { return m[i, j]; }
            set { m[i, j] = value; }
        }
        public int size
        {
            get { return n; }
        }
    }
}
