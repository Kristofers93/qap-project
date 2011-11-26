using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BA
{
    class Program
    {
        static void Main(string[] args)
        {
            BeesAlgorithm BA = new BeesAlgorithm(1000, 100, 30, 40, 20, 0.3, 1000);
            BA.ReleaseTheBees();

            /*foreach(int i in BA.costs)
                Console.WriteLine(i);*/
            Console.In.Read();
        }
    }
}
