using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pszczeli
{
    public class Parameters : Structures.IParameters
    {
        public int insects { get; set; }
        public int iterations { get; set; }
        public int reportEveryIterations { get; set; }
        public int m { get; set; }
        public int e { get; set; }
        public int nep { get; set; }
        public int nsp { get; set; }
        public float ngh { get; set; }

        public Parameters()
        {
            //Tu można zmieniać i dopisać domyślne parametry dla danego algorytmu
            insects = 1000;
            iterations = 1000;
            reportEveryIterations = 10;
        }

        public Parameters Clone()
        {
            return (Parameters)this.MemberwiseClone();
        }
    }
}
