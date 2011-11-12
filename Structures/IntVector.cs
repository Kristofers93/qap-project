using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structures
{
    public class IntVector
    {
        private int[] v;
        public IntVector(int n)
        {
            v = new int[n];
        }
        public int this[int i]
        {
            get { return v[i]; }
            set { v[i] = value; }
        }
        public int size
        {
            get { return v.Length; }
        }
        override public string ToString()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < size - 1; ++i)
            {
                builder.Append(this[i]).Append(", ");
            }
            builder.Append(this[size - 1]);
            return builder.ToString();
        }
    }
}
