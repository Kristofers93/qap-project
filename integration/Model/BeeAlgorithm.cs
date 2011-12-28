using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Model
{
    public class BeeAlgorithm : IAlgorithm
    {

        int totalNumberBees;
        int numberScout;
        int maxNumberVisits; // max number of times bee will visit a given food source without finding a better neighbor
        int maxNumberCycles; // one cycle represents an action by all bees in the hive
        public double probPersuasion = 0.90; // probability inactive bee is persuaded by better waggle solution
        public double probMistake = 0.01; // probability an active bee will reject a better neighbor food source OR accept worse neighbor food source
        TestData data = null; // this is the problem-specific data we want to optimize
        List<int> minimalResult;
        List<int> costs;
        BackgroundWorker _backgroundWorker;

        public int MaxNumberCycles
        {
            set { maxNumberCycles = value; }
            get { return maxNumberCycles; }
        }

        public int TotalNumberBees
        {
            set { totalNumberBees = value; }
            get { return totalNumberBees; }
        }

        public int NumberScout
        {
            set { numberScout = value; }
            get { return numberScout; }
        }

        public int MaxNumberVisits
        {
            set { maxNumberVisits = value; }
            get { return maxNumberVisits; }
        }

        public double ProbPersuasion
        {
            set { probPersuasion = value; }
            get { return probPersuasion; }
        }

        public double ProbMistake
        {
            set { probMistake = value; }
            get { return probMistake; }
        }

        public BeeAlgorithm()
        {
            this.totalNumberBees = 1000;
            this.numberScout = 200;
            this.maxNumberVisits = 50;
            this.maxNumberCycles = 1000;
            this.probPersuasion = 0.9F;
            this.probMistake = 0.01F;
        }

        public BeeAlgorithm(int totalNumberBees, int numberScout, int maxNumberVisits, int maxNumberCycles,
            double probPersuasion, double probMistake)
        {
            this.totalNumberBees = totalNumberBees;
            this.numberScout = numberScout;
            this.maxNumberVisits = maxNumberVisits;
            this.maxNumberCycles = maxNumberCycles;
            this.probPersuasion = probPersuasion;
            this.probMistake = probMistake;
        }


        //ustawianie danych, macierze A i B z modelu i ich rozmiar,
        public void SetTestData(int[,] A, int[,] B, int numberOfInstances)
        {
            this.data = new TestData(A, B, numberOfInstances);
        }


        public void RunAlgorithm()
        {

            if (data != null)
            {
                Hive hive = new Hive(this._backgroundWorker,totalNumberBees, numberScout, maxNumberVisits, maxNumberCycles, probPersuasion, probMistake, data);
                hive.Solve();
                this.minimalResult = new List<int>(hive.BestMemoryMatrix);
                this.costs = new List<int>(hive.Costs);
            }
            else throw new ArgumentNullException();

        }


        //lista reprezentujaca najbardziej optymalne rozwiazanie
        public List<int> ReturnMinimalResult()
        {
            return this.minimalResult;
        }

        //najmniejszy zwracany koszt
        public int GetMinimalCost()
        {
            return this.costs[this.maxNumberCycles - 1];
        }

        

        //zwraca liste parametrow
        public List<string> GetParameterNames()
        {
            throw new NotImplementedException();
        }

        //zwraca słownik zawierający wszystkie wartości domyślne parametrów
        public Dictionary<string, double> GetParameterValues()
        {
            throw new NotImplementedException();
        }

        //ustawianie niedomyślnych parametrów, klucz - nazwa,
        public void SetParameters(Dictionary<string, double> parameters)
        {
            throw new NotImplementedException();
        }


        public void addBackgroundWorker(BackgroundWorker worker)
        {
            this._backgroundWorker = worker;
        }

        public int GetCurrentCost() {
            return 0;
        }
    }
}