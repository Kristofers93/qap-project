﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.ComponentModel;
namespace Model
{
    public class FireflyAlgorithm : IAlgorithm
    {
        
        /*imax - liczba iteracji, m - liczba swietlikow, gamma - wspolcz. absorpcji, alfa - random step weight*/
        private int imax;
        private int m;
        private double gamma;    // >= 0
        private int alfa;        //{0,1, ..., n-1} albo {1,2,...,n} ??
        private int[,] A;        /*nxn, przeplyw miedzy osrodkami*/
        private int[,] B;        /*nxn, odleglosc miedzy osrodkami*/
        private int n;           /*liczba osrodkow*/
        private BackgroundWorker _backgroundWorker;
        

        //gettery settery
        public int Imax { get { return imax; } set { imax = value; } }
        public int M { get { return m; } set { m = value; } }
        public int Alfa { get { return alfa; } set { alfa = value; } }
        public double Gamma { get { return gamma; } set { gamma = value; } }
        public int CurrentIteration { get; private set; }
        public bool IsInitialized { get; private set; }
        public bool HasFinished { get; private set; }

        private readonly Random rand;    //Sys.Security.Cryptography!
        private int[][] x;
        private int minimum = 0;
        private int bestEver = 0;
        private int[] bestEverSolution;

        public FireflyAlgorithm()
        {
            rand = new Random();

        }

        
        public void InitializeAlgorithm()
        {
            bestEverSolution = new int[n];
            x = new int[m][];

            /*losowanie wejsciowych permutacji*/
            for (int i = 0; i < m; i++)
            {
                x[i] = new int[n];
                for (int j = 0; j < n; j++)
                {
                    int tmp;
                    bool used = true;

                    tmp = rand.Next(n);
                    while (used)
                    {
                        bool again = false;
                        for (int k = 0; k < j; k++)
                        {
                            if (x[i][k] == tmp)
                            {
                                again = true;
                            }
                        }
                        if (again)
                        {
                            tmp = rand.Next(n);
                        }
                        else
                        {
                            used = false;
                        }
                    }
                    x[i][j] = tmp;
                }
            }

            IsInitialized = true;
        }

        //lista reprezentujaca najbardziej optymalne rozwiazanie
        public List<int> ReturnMinimalResult()
        {
            if(!HasFinished)
                throw new Exception("I'm not finished yet!");
            return bestEverSolution.ToList<int>();
        }

        //najmniejszy zwracany koszt
        public int GetMinimalCost()
        {
            if(!HasFinished)
                throw new Exception("I'm not finished yet!");
            return bestEver;
        }

        //koszt dla danej iteracji
        public int GetCurrentCost()
        {
            throw new NotImplementedException();
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
   

        //ustawianie danych, macierze A i B z modelu i ich rozmiar,
        public void SetTestData(int[,] _A, int[,] _B, int numberOfInstances)
        {
            this.A = _A;
            this.B = _B;
            this.n = numberOfInstances;
        }

        //uruchomienie obliczen, zwraca 0 w przypadku konca obliczen?
        public void RunAlgorithm()
        {

            if (!IsInitialized) InitializeAlgorithm();

            CurrentIteration = 0;

            while (CurrentIteration++ < imax)
            {
                // aktualnie najlepsze rozwiazanie
                for (int i = 0; i < m; i++)
                {
                    if (Reward(x[i], A, B) < Reward(x[minimum], A, B))
                    {
                        minimum = i;
                    }
                }

                
                // zblizanie swietlikow do mocniej swiecacych
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (Reward(x[i], A, B) < Reward(x[j], A, B))
                        {
                            int dist = Distance(x[i], x[j]);
                            double beta = Attractiveness(gamma, dist);
                            //zbliz swietlika xi do xj
                            x[i] = Approach(beta, x[i], x[j]);
                            x[i] = PerformRandomFlight(x[i], alfa);
                        }
                    }
                }

                int rewmin = Reward(x[minimum], A, B);

                if (bestEver == 0) {
                    bestEverSolution = x[minimum]; 
                    bestEver = rewmin;
                }

                if (rewmin < bestEver)
                {
                    bestEverSolution = x[minimum];   
                    bestEver = rewmin;
                }

                if (_backgroundWorker != null) _backgroundWorker.ReportProgress(bestEver);
                if (cancel) break;
                // aktualnie najlepsze rozw porusza sie losowo

                x[minimum] = PerformRandomFlight(x[minimum], alfa);
                //if(CurrentIteration % 100 == 0) Console.Write(".");
                
            }
            HasFinished = true;
           

        }

     
        private int[] PerformRandomFlight(int[] xi, double alfa)
        {
            
            int[] result = new int[n];
            xi.CopyTo(result, 0);
            
            if(alfa < 0 || alfa >= n ) throw new Exception("Zla alfa");
            var rand = new Random();
            
            int nrOfElementsToShuffle = (int) (alfa*rand.NextDouble());

            //choosing elements to be shuffled
            var positionsToShuffle = new List<int>(); 
            var needed = nrOfElementsToShuffle;
            var available = n;
            
            while (positionsToShuffle.Count < nrOfElementsToShuffle) {
               if( rand.NextDouble() < ((double)needed / available) )
               {
                  positionsToShuffle.Add(xi[available - 1]);
                  needed--;
               }
               available--;
            }

            //CHANGEME
            //shuffling them
            var valuesLeft = positionsToShuffle.Select(i => xi[i]).ToList();

            int left = nrOfElementsToShuffle;
            foreach (int i in positionsToShuffle)
            {
                result[i] = valuesLeft[rand.Next(left)];
//                valuesLeft.Remove(xi[i]);
                valuesLeft.Remove(result[i]);
                left--;
            }
            
            if(left != 0) throw new Exception("nie powinnno nic zostac!");

            return result;
        }

        private int[] Approach(double beta, int[] xi, int[] xj)
        {
            //var rand = new Random();
            //var n = xi.Length;
            var result = new int[n]; //result
            HashSet<int> valuesLeft = new HashSet<int>(Enumerable.Range(0, n)); // {0, 1, ..., n-1} possible values
            var gaps = new List<int>(); //indFree  //indexes of gaps which still need to be filled
            var gapsToRandomize = new List<int>(); //gaps for which there is no rescue

            // najpierw przepisujemy te miejsca ktore sie zgadzaja
            // tworzymy zbior wartosci do obsadzenia i miejsc w ktorych mozna obsadzic wartosci
            for (int i = 0; i < n; i++)
            {
                if (xi[i] == xj[i])
                {
                    result[i] = xi[i];
//                    valuesLeft.Remove(result[i]);
                    if (!valuesLeft.Remove(result[i]))
                        throw new Exception("Blad - wartosci mialy sie nie powtarzac!");
                }
                else
                {
                    result[i] = -1;
                    gaps.Add(i);
                }
            }
            while (gaps.Count > 0)
            {
                int tmp = gaps[rand.Next(gaps.Count)];
                int pickedValue = rand.NextDouble() < beta ? xj[tmp] : xi[tmp];
                if(valuesLeft.Contains(pickedValue))
                {
                    result[tmp] = pickedValue;
                    if(!valuesLeft.Remove(result[tmp]) )
                        throw new Exception("Blad - wartosci mialy sie nie powtarzac!");
                }
                else
                {
                    gapsToRandomize.Add(tmp);
                }
                gaps.Remove(tmp);
            }

            if (gapsToRandomize.Count + 1 == valuesLeft.Count()) throw new Exception("Dokladnie o 1 wiecej wartosci!");
            if(gapsToRandomize.Count != valuesLeft.Count()) throw new Exception("Liczba pozostalych luk jest inna niz liczba pozostalych wartosci!");
            var valuesLeftAsList = valuesLeft.ToList();
            foreach (var gap in gapsToRandomize)
            {
                result[gap] = valuesLeftAsList.ElementAt(rand.Next(valuesLeftAsList.Count));
                valuesLeftAsList.Remove(result[gap]);
            }
            if(valuesLeftAsList.Any()) throw new Exception("blad : zostaly jakies wartosci");

            return result;
        }

        private double Attractiveness(double gamma, int dist)
        {
            return 1/(1 + gamma*dist*dist);
        }

        private int Distance(int[] xi, int[] xj)
        {
            //var n = xi.Length;
            var d = 0;
            for (var k = 0; k < n; k++)
            {
                if (xi[k] != xj[k])
                {
                    d += 1;
                }
            }
            return d;
        }

        private int Reward(int[] solut, int[,] A, int[,] B)
        {
            int sum = 0;
            //int n = A.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sum += A[i,j]*B[solut[i],solut[j]];
                }
            }

            return sum;
        }

        public void addBackgroundWorker(BackgroundWorker worker)
        {
            this._backgroundWorker = worker;
        }

        private bool cancel;
        public void CancelAlgorithm()
        {
            cancel = true;
        }
    }
}