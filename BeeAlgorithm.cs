using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pszczeli
{
    public class BeeAlgorithm : IAlgorithm
    {

        public void runAlgorithm()
        {
            try
            {
                Console.WriteLine("\nBegin Simulated Bee Colony algorithm demo\n");
                Console.WriteLine("Loading cities data for SBC Traveling Salesman Problem analysis");
                TestData data = new TestData(20);
                Console.WriteLine(data.ToString());
                Console.WriteLine("Number of places = " + data.places.Length);
                Console.WriteLine("Number of possible paths = " + data.NumberOfPossiblePaths().ToString("#,###"));
                Console.WriteLine("Best possible solution (shortest path) length = " + data.ShortestPathLength().ToString("F4"));

                int totalNumberBees = 100;
                int numberInactive = 20;
                int numberActive = 50;
                int numberScout = 30;
                int maxNumberVisits = 100;
                int maxNumberCycles = 3460;

                Hive hive = new Hive(totalNumberBees, numberInactive, numberActive, numberScout, maxNumberVisits, maxNumberCycles, data);
                Console.WriteLine("\nInitial random hive");
                Console.WriteLine(hive);

                bool doProgressBar = true;
                hive.Solve(doProgressBar);

                Console.WriteLine("\nFinal hive");
                Console.WriteLine(hive);

                Console.WriteLine("End Simulated Bee Colony demo");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal: " + ex.Message);
                Console.ReadLine();
            }

        }


    }
}
