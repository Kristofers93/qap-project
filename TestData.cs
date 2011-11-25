using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pszczeli
{
    class TestData
    {
        public char[] places;

        public TestData(int numberPlaces)
        {
            this.places = new char[numberPlaces];
            this.places[0] = 'A';
            for (int i = 1; i < this.places.Length; ++i)
                this.places[i] = (char)(this.places[i - 1] + 1);
        }
        public double Distance(char firstPlace, char secondPlace)
        {
            if (firstPlace < secondPlace)
                return 1.0 * ((int)secondPlace - (int)firstPlace);
            else
                return 1.5 * ((int)firstPlace - (int)secondPlace);
        }
        public double ShortestPathLength()
        {
            return 1.0 * (this.places.Length - 1);
        }
        public long NumberOfPossiblePaths()
        {
            long n = this.places.Length;
            long answer = 1;
            for (int i = 1; i <= n; ++i)
                checked { answer *= i; }
            return answer;
        }
        public override string ToString()
        {
            string s = "";
            s += "Places: ";
            for (int i = 0; i < this.places.Length; ++i)
                s += this.places[i] + " ";
            return s;
        }
    } // class TestData
}
