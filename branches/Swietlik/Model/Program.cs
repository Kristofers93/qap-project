using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> l = Enumerable.Range(0, 15).ToList();
            foreach (var i in l)
            {
                Console.WriteLine(i);
            }

            

        }
    }
}
