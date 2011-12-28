using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace Model
{
    public class BeesAlgorithm : IAlgorithm
    {
        private int n;                      // rozmiar macierzy
        private int nb;                     // liczba pszczol
        private int m;                      // wybranych miejsc
        private int e;                      // lepszych miejsc
        private int nep;                    // liczba pszczol dla lepszych miejsc
        private int nsp;                    // liczba pszczol dla wybranych miesc
        private double ngh;                 // rozmiar sasiedztwa
        private int imax;                   // maksymalna liczba iteracji
        private BackgroundWorker _backgroundWorker;

        public int Nb {
            get { return nb; }
            set { nb = value;  }
        }

        public int Imax
        {
            get { return imax; }
            set { imax = value; }
        }

        public int M
        {
            get { return m; }
            set { m = value; }
        }

        public int E
        {
            get { return e; }
            set { e = value; }
        }

        public int Nsp
        {
            get { return nsp; }
            set { nsp = value; }
        }

        public int Nep
        {
            get { return nep; }
            set { nep = value; }
        }

        public double Ngh {
            get { return ngh; }
            set { ngh = value; }
        }

        private int selsit;                 // liczba wybranych - liczba lepszych
        private int neighbourhoodSize;      // rozmiar sasiedztwa przeliczony z procentow
        private Bee[] bees;                 // tablica wszystkich pszczol
        private int minimalCost;            // najmniejszy koszt
        private int[] bestResult;           // najlepsza permutacja
        private bool finished = false;      // czy skonczyl
        public List<int> costs = new List<int>();   // koszty dla poszczegolnych iteracji

      
        private void Init()
        {
            // tablica z liczbami dla kazdej pszczoly  
            int[] initialTable = new int[n];
            for (int i = 0; i < n; i++)
            {
                initialTable[i] = i;
            }

            // tworzymy pszczoly
            bees = new Bee[nb];
            for (int i = 0; i < nb; i++)
            {
                bees[i] = new Bee((int[])initialTable.Clone());         // kazda pszczola otrzymuje swoja kopie tablicy liczb
                bees[i].FindSomeSite(n);                                // i ja losowo permutuje
            }

            // sortujemy pszczoly wedlug kosztu wyliczonego z posiadanych przez nie permutacji
            Array.Sort<Bee>(bees,Bee.Compare);
            SaveBestResult();
        }

        public void ReleaseTheBees()
        {
            Init();
            neighbourhoodSize = (int) Math.Round(n * ngh);              // wyliczamy rozmiar sasiedztwa
            for (int iter = 0; iter < imax; iter++)               
            {
                // przetwarzamy najlepsze pszczoly
                for (int bestSites = 0; bestSites < e; bestSites++)
                {
                    Bee[] nepBees = new Bee[nep];
                    for (int i = 0; i < nep; i++)
                    {
                        nepBees[i] = new Bee(bees[bestSites].Data);
                        nepBees[i].FindSomeSite(neighbourhoodSize);
                    }
                    Bee minimalBee = GetMinimalBee(nepBees);
                    if(minimalBee.Value < bees[bestSites].Value)
                        bees[bestSites] = minimalBee;
                }

                // pozostale wybrane pszczoly
                for (int selectedSites = e; selectedSites < m; selectedSites++)
                {
                    Bee[] nspBees = new Bee[selsit];
                    for (int i = 0; i < selsit; i++)
                    {
                        nspBees[i] = new Bee(bees[selectedSites].Data);
                        nspBees[i].FindSomeSite(neighbourhoodSize);
                    }
                    Bee minimalBee = GetMinimalBee(nspBees);
                    if (minimalBee.Value < bees[selectedSites].Value)
                        bees[selectedSites] = minimalBee;
                }

                // pozostale wysylamy losowo
                for (int randomSites = m; randomSites < nb; randomSites++)
                {
                    bees[randomSites].FindSomeSite(n);
                }

                // sortujemy
                Array.Sort<Bee>(bees, Bee.Compare);
                if (minimalCost > bees[0].Value)
                {
                    SaveBestResult();
                }

                //jeśli został przypisany background worker to notyfikuje go
                if (_backgroundWorker != null) _backgroundWorker.ReportProgress(minimalCost);
                
            }

            finished = true;
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
            minimalCost = bees[0].Value;
            bestResult = bees[0].Data;
            costs.Add(minimalCost);
        }

        
        public List<int> ReturnMinimalResult()
        {
            if (!finished)
                throw new Exception("I'm not finished yet!");
            return bestResult.ToList();
        }

        public int GetMinimalCost()
        {
            if (!finished)
                throw new Exception("I'm not finished yet!");
            return minimalCost;
        }

        public int GetCurrentCost()
        {
            return 0;
        }

        public List<string> GetParameterNames()
        {
            List<String> pNames = new List<String>();
            pNames.Add("n");
            pNames.Add("m");
            pNames.Add("e");
            pNames.Add("nep");
            pNames.Add("nsp");
            pNames.Add("ngh");
            pNames.Add("imax");
            return pNames;
        }

        public Dictionary<string, double> GetParameterValues()
        {
            Dictionary<string, double> pValues = new Dictionary<string, double>();

            pValues.Add("n", 100);
            pValues.Add("m", 10);
            pValues.Add("e", 3);
            pValues.Add("nep", 40);
            pValues.Add("nsp", 20);
            pValues.Add("ngh", 0.2);
            pValues.Add("imax", 1000);

            return pValues;

        }

        public void SetTestData(int[,] A, int[,] B, int numberOfInstances)
        {
            this.n = numberOfInstances;
            Matrix.A = A;
            Matrix.B = B;
            Matrix.n = numberOfInstances;
        }

        public void RunAlgorithm()
        {
            selsit = m - e;
            Init();
            ReleaseTheBees();
        }

        public void addBackgroundWorker(BackgroundWorker worker)
        {
            this._backgroundWorker = worker;
        }
    }
}
