using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            FireflyTest faTest = new FireflyTest();
            faTest.RunTest("D:\\Dokumenty\\Visual Studio\\integration\\QAPSolution1.0\\TestInstances\\bur26a.dat");

            Console.Read();
        }
    }
}
