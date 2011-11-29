using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BA
{
    public class Bee
    {
        private int[] myData;
        private int n;
        Random rand = new Random();

        private int myValue = -1;

        public int Value
        {
            get { return myValue; }
        }

        public int[] Data
        {
            get { return (int[])myData.Clone(); }
        }

        public Bee(int[] data)
        {
            myData = data;
            n = Matrix.n;
        }

        // losowanie permutacji (dla nbSize = n - losowo, dla nbSize malego - szukanie w sasiedztwie)
        public void FindSomeSite(int neighbourhoodSize)
        {
            for (int i = 0; i < neighbourhoodSize/2; i++)
            {
                int x = rand.Next(n);
                int y = rand.Next(n);
                while (y == x) y = rand.Next(n);
                int temp = myData[x];
                myData[x] = myData[y];
                myData[y] = temp;
            }
            myValue = Reward(Matrix.A, Matrix.B);
        }

        // liczenie funkcji celu
        private int Reward(int[,] A, int[,] B)
        {
            int sum = 0;
            //CopyPasted

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sum += A[i, j] * B[myData[i], myData[j]];
                }
            }
            return sum;
        }

        // komparator przydatnosci pszczol
        public static int Compare(Bee b1, Bee b2)
        {
            return b1.Value - b2.Value;
        }
    }
}
