using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Structures
{
    public interface IParameters
    {
        int insects { get; set; }
        int iterations { get; set; }
        int reportEveryIterations { get; set; }
    }
}
