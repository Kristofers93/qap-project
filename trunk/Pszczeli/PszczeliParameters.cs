using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pszczeli
{
    public class PszczeliParameters : Structures.IParameters
    {
        public int insectAmount { get; set; }
        public int iterations { get; set; }
        //TODO Dopisać parametry charakterystyczne dla algorytmu
    }
}
