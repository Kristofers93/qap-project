using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pszczeli
{
    class Bee
    {
        public int status; // 0 = inactive, 1 = active, 2 = scout
        public char[] memoryMatrix; // problem-specific. a path of cities.
        public double measureOfQuality; // smaller values are better. total distance of path.
        public int numberOfVisits;

        public Bee(int status, char[] memoryMatrix, double measureOfQuality, int numberOfVisits)
        {
            this.status = status;
            this.memoryMatrix = new char[memoryMatrix.Length];
            Array.Copy(memoryMatrix, this.memoryMatrix, memoryMatrix.Length);
            this.measureOfQuality = measureOfQuality;
            this.numberOfVisits = numberOfVisits;
        }

        public override string ToString()
        {
            string s = "";
            s += "Status = " + this.status + "\n";
            s += " Memory = " + "\n";
            for (int i = 0; i < this.memoryMatrix.Length - 1; ++i)
                s += this.memoryMatrix[i] + "->";
            s += this.memoryMatrix[this.memoryMatrix.Length - 1] + "\n";
            s += " Quality = " + this.measureOfQuality.ToString("F4");
            s += " Number visits = " + this.numberOfVisits;
            return s;
        }
    } // Bee
}
