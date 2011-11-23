using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class FireflyAlgorithm : IAlgorithm
    {
        //lista reprezentujaca najbardziej optymalne rozwiazanie
        public IList<int> ReturnMinimalResult()
        {
            throw new NotImplementedException();
        }

        //najmniejszy zwracany koszt
        public int GetMinimalCost()
        {
            throw new NotImplementedException();
        }

        //koszt dla danej iteracji
        public int GetCurrentCost(int iterationNumber)
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

        //ustawianie niedomyślnych parametrów, klucz - nazwa,
        public void SetParameters(Dictionary<string, double> parameters)
        {
            throw new NotImplementedException();
        }

        //ustawianie danych, macierze A i B z modelu i ich rozmiar,
        public void SetTestData(int[,] A, int[,] B, int numberOfInstances)
        {
            throw new NotImplementedException();
        }

        //uruchomienie obliczen, zwraca 0 w przypadku konca obliczen?
        public int RunAlgorithm()
        {
            return 0;
        }

        // imax - liczba iteracji, m - liczba swietlikow, bet0 - max atrakcynosc, gamma - wspolcz. absorpcji, alfa - random step weight
        // void- wypiszmy wynik na ekran
        public void TmpRun(int imax, int m, float beta0, float gamma, float alfa)
        {
            int n = 10; //wymiar macierzy, dlugosc permutacji
            Random rand = new Random();
            int currentIter = 0;
            int min = 0;
            int[][] x = new int[m][];
            
            //init
            for (int i=0; i<m; i++)
            {
                x[i] = new int[n];
                for (int j=0; j<n; j++)
                {
                    x[i][j] = rand.Next(n);
                }
            }

            while (currentIter++ < imax)
            {
                // aktualnie najlepsze rozwiazanie
                for (int i=0; i<m; i++)
                {
                    if (Reward(x[i]) < Reward(x[min]))
                    {
                        min = i;
                    }
                }

                // zblizanie swietlikow do mocniej swiecacych
                for (int i=0; i<m; i++)
                {
                    for (int j=0; j<m; j++)
                    {
                        if (Reward(x[i])<Reward(x[j]))
                        {
                            
                            int dist = Distance(x[i], x[j]);
                            int beta = Attractiveness(beta0, gamma,dist);
                            //zbliz swietlika xi do xj
                            x[i] = Approach(beta, x[i], x[j]);
                            x[i] = PerformRandomFlight(alfa);

                        }
                    }
                }

                // aktualnie najlepsze rozw porusza sie losowo

                x[min] = PerformRandomFlight(alfa);

            }

            Console.WriteLine(x[min].ToString());

        }

        private int[] PerformRandomFlight(float alfa)
        {
            throw new NotImplementedException();
        }

        private int[] Approach(int beta, int[] p, int[] p_2)
        {
            throw new NotImplementedException();
        }

        private int Attractiveness(float beta0, float gamma, int dist)
        {
            throw new NotImplementedException();
        }

        private int Distance(int[] p, int[] p_2)
        {
            throw new NotImplementedException();
        }

        private int Reward(int[] x)
        {
            throw new NotImplementedException();
        }
    }
}
