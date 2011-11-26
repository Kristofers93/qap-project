using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BA
{
    public class BeesAlgorithm
    {
        private int n; // matrix size
        private int nb; // number of bees
        private int m;
        private int e;
        private int nep;
        private int nsp;
        private double ngh;
        private int imax;


        private int selsit;
        private int neighbourhoodSize;
        private Bee[] bees;
        private int minimalCost;
        private int[] bestResult;
        public List<int> costs = new List<int>();

        public BeesAlgorithm(int n, int m, int e, int nep, int nsp, double ngh, int imax)
        {
            this.nb = n;
            this.m = m;
            this.e = e;
            this.nep = nep;
            this.nsp = nsp;
            this.ngh = ngh;
            this.imax = imax;
            selsit = m - e;
            this.n = Matrix.n;
        }

        private void Init()
        {
                        
            int[] initialTable = new int[n];
            for (int i = 0; i < n; i++)
            {
                initialTable[i] = i;
            }
            bees = new Bee[nb];
            for (int i = 0; i < nb; i++)
            {
                bees[i] = new Bee((int[])initialTable.Clone());
                bees[i].FindSomeSite(n);
            }
            Array.Sort<Bee>(bees,Bee.Compare);
            minimalCost = bees[0].Value;
            bestResult = bees[0].Data;
            costs.Add(minimalCost);
        }

        public void ReleaseTheBees()
        {
            Init();
            neighbourhoodSize = (int) Math.Round(n * ngh);
            for (int iter = 0; iter < imax; iter++)               
            {
                for (int bestSites = 0; bestSites < e; bestSites++)
                {
                    Bee[] nepBees = new Bee[nep];
                    for (int i = 0; i < nep; i++)
                    {
                        nepBees[i] = new Bee(bees[bestSites].Data);
                        nepBees[i].FindSomeSite(neighbourhoodSize);
                    }
                    bees[bestSites] = GetMinimalBee(nepBees);
                }

                for (int selectedSites = e; selectedSites < m; selectedSites++)
                {
                    Bee[] nspBees = new Bee[selsit];
                    for (int i = 0; i < selsit; i++)
                    {
                        nspBees[i] = new Bee(bees[selectedSites].Data);
                        nspBees[i].FindSomeSite(neighbourhoodSize);
                    }
                    bees[selectedSites] = GetMinimalBee(nspBees);
                }

                for (int randomSites = m; randomSites < nb; randomSites++)
                {
                    bees[randomSites].FindSomeSite(n);
                }

                Array.Sort<Bee>(bees, Bee.Compare);
                SaveBestResult();
            }


        }

        private Bee GetMinimalBee(Bee[] beesArray)
        {
            Bee minBee = beesArray[0];
            for (int i = 1; i < beesArray.Length; i++)
            {
                if (Bee.Compare(minBee, beesArray[i]) > 0)
                    minBee = beesArray[i];
            }
            return minBee;
        }

        private void SaveBestResult()
        {
            if (minimalCost > bees[0].Value)
            {
                minimalCost = bees[0].Value;
                bestResult = bees[0].Data;
            }
            Console.WriteLine(minimalCost);
            costs.Add(minimalCost);
        }
    }
}
