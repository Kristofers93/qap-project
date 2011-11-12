using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swietlikowy
{
    public class Parameters : Structures.IParameters
    {
        public int insects { get; set; }
        public int iterations { get; set; }
        public int reportEveryIterations { get; set; }
        //TODO Dopisać parametry charakterystyczne dla algorytmu

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
