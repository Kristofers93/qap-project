using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Bee
{
    class Bee
    {
        public int status; // 0 = inactive, 1 = active, 2 = scout
        public int[] memoryMatrix; // problem-specific. a path of cities.
        public int measureOfQuality; // smaller values are better. total distance of path.
        public int numberOfVisits;

        public Bee(int status, int[] memoryMatrix, int measureOfQuality, int numberOfVisits)
        {
            this.status = status;
            this.memoryMatrix = new int[memoryMatrix.Length];
            Array.Copy(memoryMatrix, this.memoryMatrix, memoryMatrix.Length);
            this.measureOfQuality = measureOfQuality;
            this.numberOfVisits = numberOfVisits;
        }

    } // Bee
}
