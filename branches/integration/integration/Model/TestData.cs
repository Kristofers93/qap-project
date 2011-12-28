using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    class TestData
    {

        int[,] A; // Flow matrix
        int[,] B; // Distance matrix
        int n; // problem size



        public TestData(int[,] A, int[,] B, int problemSize)
        {
            this.A = A;
            this.B = B;
            this.n = problemSize;
        }

        public int Cost(int l1, int l2, int f1, int f2)
        {
            if (l1 < n && l2 < n && f1 < n && f2 < n)
                return A[f1, f2] * B[l1, l2];
            else throw new IndexOutOfRangeException();
        }

        public int GetProblemSize()
        {
            return n;
        }



    } // class TestData
}