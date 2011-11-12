using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structures
{
    public interface IResultViewModel
    {
        void reportPartialResult(int iteration, int cost);
        void reportFinalResult(IntVector vector, int cost);
    }
}
