﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Bee
{
    class Hive
    {
        static Random random = null; // multipurpose

        public TestData data; // this is the problem-specific data we want to optimize

        public int totalNumberBees; // mostly for readability in the object constructor call
        public int numberInactive;
        public int numberActive;
        public int numberScout;

        public int maxNumberCycles; // one cycle represents an action by all bees in the hive
        //public int maxCyclesWithNoImprovement; // deprecated

        public int maxNumberVisits; // max number of times bee will visit a given food source without finding a better neighbor
        public double probPersuasion = 0.90; // probability inactive bee is persuaded by better waggle solution
        public double probMistake = 0.01; // probability an active bee will reject a better neighbor food source OR accept worse neighbor food source

        public Bee[] bees;
        public char[] bestMemoryMatrix; // problem-specific
        public double bestMeasureOfQuality;
        public int[] indexesOfInactiveBees; // contains indexes into the bees array

        public override string ToString()
        {
            string s = "";
            s += "Best path found: ";
            for (int i = 0; i < this.bestMemoryMatrix.Length - 1; ++i)
                s += this.bestMemoryMatrix[i] + "->";
            s += this.bestMemoryMatrix[this.bestMemoryMatrix.Length - 1] + "\n";

            s += "Path quality:    ";
            if (bestMeasureOfQuality < 10000.0)
                s += bestMeasureOfQuality.ToString("F4") + "\n";
            else
                s += bestMeasureOfQuality.ToString("#.####e+00");
            s += "\n";
            return s;
        }

        public Hive(int totalNumberBees, int numberInactive, int numberActive, int numberScout, int maxNumberVisits,
          int maxNumberCycles, TestData data)
        {
            random = new Random(0);

            this.totalNumberBees = totalNumberBees;
            this.numberInactive = numberInactive;
            this.numberActive = numberActive;
            this.numberScout = numberScout;
            this.maxNumberVisits = maxNumberVisits;
            this.maxNumberCycles = maxNumberCycles;
            //this.maxCyclesWithNoImprovement = maxCyclesWithNoImprovement;

            //this.data = new TestData(data.cities.Length); // hive's copy of problem-specific data
            this.data = data; // reference to CityData

            // this.probPersuasion & this.probMistake are hard-coded in class definition

            this.bees = new Bee[totalNumberBees];
            this.bestMemoryMatrix = GenerateRandomMemoryMatrix(); // alternative initializations are possible
            this.bestMeasureOfQuality = MeasureOfQuality(this.bestMemoryMatrix);

            this.indexesOfInactiveBees = new int[numberInactive]; // indexes of bees which are currently inactive

            for (int i = 0; i < totalNumberBees; ++i) // initialize each bee, and best solution
            {
                int currStatus; // depends on i. need status before we can initialize Bee
                if (i < numberInactive)
                {
                    currStatus = 0; // inactive
                    indexesOfInactiveBees[i] = i; // curr bee is inactive
                }
                else if (i < numberInactive + numberScout)
                {
                    currStatus = 2; // scout
                }
                else
                {
                    currStatus = 1; // active
                }

                char[] randomMemoryMatrix = GenerateRandomMemoryMatrix();
                double mq = MeasureOfQuality(randomMemoryMatrix);
                int numberOfVisits = 0;

                bees[i] = new Bee(currStatus, randomMemoryMatrix, mq, numberOfVisits); // instantiate current bee

                // does this bee have best solution?
                if (bees[i].measureOfQuality < bestMeasureOfQuality) // curr bee is better (< because smaller is better)
                {
                    Array.Copy(bees[i].memoryMatrix, this.bestMemoryMatrix, bees[i].memoryMatrix.Length);
                    this.bestMeasureOfQuality = bees[i].measureOfQuality;
                }
            } // each bee

        } // TravelingSalesmanHive ctor


        public char[] GenerateRandomMemoryMatrix()
        {
            char[] result = new char[this.data.places.Length]; // // problem-specific
            Array.Copy(this.data.places, result, this.data.places.Length);

            for (int i = 0; i < result.Length; i++) // Fisher-Yates (Knuth) shuffle
            {
                int r = random.Next(i, result.Length);
                char temp = result[r]; result[r] = result[i]; result[i] = temp;
            }
            return result;
        } // GenerateRandomMemoryMatrix()

        public char[] GenerateNeighborMemoryMatrix(char[] memoryMatrix)
        {
            char[] result = new char[memoryMatrix.Length];
            Array.Copy(memoryMatrix, result, memoryMatrix.Length);

            int ranIndex = random.Next(0, result.Length); // [0, Length-1] inclusive
            int adjIndex;
            if (ranIndex == result.Length - 1)
                adjIndex = 0;
            else
                adjIndex = ranIndex + 1;

            char tmp = result[ranIndex];
            result[ranIndex] = result[adjIndex];
            result[adjIndex] = tmp;

            return result;
        } // GenerateNeighborMemoryMatrix()

        public double MeasureOfQuality(char[] memoryMatrix)
        {
            double answer = 0.0;
            for (int i = 0; i < memoryMatrix.Length - 1; ++i)
            {
                char c1 = memoryMatrix[i];
                char c2 = memoryMatrix[i + 1];
                double d = this.data.Distance(c1, c2);
                answer += d;
            }
            return answer;
        } // MeasureOfQuality()

        public void Solve(bool doProgressBar) // find best Traveling Salesman Problem solution
        {
            bool pb = doProgressBar; // just want a shorter variable
            int numberOfSymbolsToPrint = 10; // 10 units so each symbol is 10.0% progress
            int increment = this.maxNumberCycles / numberOfSymbolsToPrint;
            if (pb) Console.WriteLine("\nEntering SBC Traveling Salesman Problem algorithm main processing loop\n");
            if (pb) Console.WriteLine("Progress: |==========|"); // 10 units so each symbol is 10% progress
            if (pb) Console.Write("           ");
            int cycle = 0;

            while (cycle < this.maxNumberCycles)
            {
                for (int i = 0; i < this.totalNumberBees; ++i) // each bee
                {
                    if (this.bees[i].status == 1) // active bee
                        ProcessActiveBee(i);
                    else if (this.bees[i].status == 2) // scout bee
                        ProcessScoutBee(i);
                    else if (this.bees[i].status == 0) // inactive bee
                        ProcessInactiveBee(i);
                } // for each bee
                ++cycle;

                // print a progress bar
                if (pb && cycle % increment == 0)
                    Console.Write("^");
            } // main while processing loop

            if (pb) Console.WriteLine(""); // end the progress bar
        } // Solve()

        private void ProcessInactiveBee(int i)
        {
            return; // not used in this implementation
        }

        private void ProcessActiveBee(int i)
        {
            char[] neighbor = GenerateNeighborMemoryMatrix(bees[i].memoryMatrix); // find a neighbor solution
            double neighborQuality = MeasureOfQuality(neighbor); // get its quality
            double prob = random.NextDouble(); // used to determine if bee makes a mistake; compare against probMistake which has some small value (~0.01)
            bool memoryWasUpdated = false; // used to determine if bee should perform a waggle dance when done
            bool numberOfVisitsOverLimit = false; // used to determine if bee will convert to inactive status

            if (neighborQuality < bees[i].measureOfQuality) // active bee found better neighbor (< because smaller values are better)
            {
                if (prob < probMistake) // bee makes mistake: rejects a better neighbor food source
                {
                    ++bees[i].numberOfVisits; // no change to memory but update number of visits
                    if (bees[i].numberOfVisits > maxNumberVisits) numberOfVisitsOverLimit = true;
                }
                else // bee does not make a mistake: accepts a better neighbor
                {
                    Array.Copy(neighbor, bees[i].memoryMatrix, neighbor.Length); // copy neighbor location into bee's memory
                    bees[i].measureOfQuality = neighborQuality; // update the quality
                    bees[i].numberOfVisits = 0; // reset counter
                    memoryWasUpdated = true; // so that this bee will do a waggle dance 
                }
            }
            else // active bee did not find a better neighbor
            {
                //Console.WriteLine("c");
                if (prob < probMistake) // bee makes mistake: accepts a worse neighbor food source
                {
                    Array.Copy(neighbor, bees[i].memoryMatrix, neighbor.Length); // copy neighbor location into bee's memory
                    bees[i].measureOfQuality = neighborQuality; // update the quality
                    bees[i].numberOfVisits = 0; // reset
                    memoryWasUpdated = true; // so that this bee will do a waggle dance 
                }
                else // no mistake: bee rejects worse food source
                {
                    ++bees[i].numberOfVisits;
                    if (bees[i].numberOfVisits > maxNumberVisits) numberOfVisitsOverLimit = true;
                }
            }

            // at this point we need to determine a.) if the number of visits has been exceeded in which case bee becomes inactive
            // or b.) memory was updated in which case check to see if new memory is a global best, and then bee does waggle dance
            // or c.) neither in which case nothing happens (bee just returns to hive).

            if (numberOfVisitsOverLimit == true)
            {
                bees[i].status = 0; // current active bee transitions to inactive
                bees[i].numberOfVisits = 0; // reset visits (and no change to this bees memory)
                int x = random.Next(numberInactive); // pick a random inactive bee. x is an index into a list, not a bee ID
                bees[indexesOfInactiveBees[x]].status = 1; // make it active
                indexesOfInactiveBees[x] = i; // record now-inactive bee 'i' in the inactive list
            }
            else if (memoryWasUpdated == true) // current bee returns and performs waggle dance
            {
                // first, determine if the new memory is a global best. note that if bee has accepted a worse food source this can't be true
                if (bees[i].measureOfQuality < this.bestMeasureOfQuality) // the modified bee's memory is a new global best (< because smaller is better)
                {
                    Array.Copy(bees[i].memoryMatrix, this.bestMemoryMatrix, bees[i].memoryMatrix.Length); // update global best memory
                    this.bestMeasureOfQuality = bees[i].measureOfQuality; // update global best quality
                }
                DoWaggleDance(i);
            }
            else // number visits is not over limit and memory was not updated so do nothing (return to hive but do not waggle)
            {
                return;
            }
        } // ProcessActiveBee()

        private void ProcessScoutBee(int i)
        {
            char[] randomFoodSource = GenerateRandomMemoryMatrix(); // scout bee finds a random food source. . . 
            double randomFoodSourceQuality = MeasureOfQuality(randomFoodSource); // and examines its quality
            if (randomFoodSourceQuality < bees[i].measureOfQuality) // scout bee has found a better solution than its current one (< because smaller measure is better)
            {
                Array.Copy(randomFoodSource, bees[i].memoryMatrix, randomFoodSource.Length); // unlike active bees, scout bees do not make mistakes
                bees[i].measureOfQuality = randomFoodSourceQuality;
                // no change to scout bee's numberOfVisits or status

                // did this scout bee find a better overall/global solution?
                if (bees[i].measureOfQuality < bestMeasureOfQuality) // yes, better overall solution (< because smaller is better)
                {
                    Array.Copy(bees[i].memoryMatrix, this.bestMemoryMatrix, bees[i].memoryMatrix.Length); // copy scout bee's memory to global best
                    this.bestMeasureOfQuality = bees[i].measureOfQuality;
                } // better overall solution

                DoWaggleDance(i); // scout returns to hive and does waggle dance

            } // if scout bee found better solution
        } // ProcessScoutBee()

        private void DoWaggleDance(int i)
        {
            for (int ii = 0; ii < numberInactive; ++ii) // each inactive/watcher bee
            {
                int b = indexesOfInactiveBees[ii]; // index of an inactive bee
                if (bees[b].status != 0) throw new Exception("Catastrophic logic error when scout bee waggles dances");
                if (bees[b].numberOfVisits != 0) throw new Exception("Found an inactive bee with numberOfVisits != 0 in Scout bee waggle dance routine");
                if (bees[i].measureOfQuality < bees[b].measureOfQuality) // scout bee has a better solution than current inactive/watcher bee (< because smaller is better)
                {
                    double p = random.NextDouble(); // will current inactive bee be persuaded by scout's waggle dance?
                    if (this.probPersuasion > p) // this inactive bee is persuaded by the scout (usually because probPersuasion is large, ~0.90)
                    {
                        Array.Copy(bees[i].memoryMatrix, bees[b].memoryMatrix, bees[i].memoryMatrix.Length);
                        bees[b].measureOfQuality = bees[i].measureOfQuality;
                    } // inactive bee has been persuaded
                } // scout bee has better solution than watcher/inactive bee
            } // each inactive bee
        } // DoWaggleDance()

    } // class ShortestPathHive

}