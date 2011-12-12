using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mrowkowy
{
    public class Parameters : Structures.IParameters
    {
        public int insects { get; set; }
        public int iterations { get; set; }
        public int reportEveryIterations { get; set; }
        public float alpha { get; set; }
        public float beta { get; set; }
        public float rho { get; set; }
        public float q { get; set; }
        public float q0 { get; set; }
        public float t0 { get; set; }
        public float Q { get; set; }
        

        public Parameters(){
            //Tu można zmieniać i dopisać domyślne parametry dla danego algorytmu
            insects = 100;
            iterations = 1000;
            reportEveryIterations = 10;
            alpha = 0.6F;
            beta = 4.2F;
            rho = 0.8F;
            q = 3.4F;
            q0 = 22.0F;
            t0 = 1.8F;
            Q = 1.0F;
        }

        public Parameters Clone(){
            return (Parameters)this.MemberwiseClone();
        }
    }
}
