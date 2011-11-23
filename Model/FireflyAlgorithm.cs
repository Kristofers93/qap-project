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

        // imax - liczba iteracji, m - liczba swietlikow, bet0 - max atrakcynosc, gamma - wspolcz. absorpcji, 
        // alfa - random step weight
        // void - wypiszmy wynik na ekran
        public void TmpRun(int imax, int m, double beta0, double gamma, double alfa, int[][] A, int[][] B)
        {
            int n = A.Length; //wymiar macierzy, dlugosc permutacji
            Random rand = new Random();
            int currentIter = 0;
            int min = 0;
            int[][] x = new int[m][]; //

            // losowanie wejsciowych permutacji
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
                        for (int k = 0; k < j; k++)
                        {
                            if (x[i][k] == tmp)
                            {
                                break;
                            }
                        }
                        used = false;
                    }
                    x[i][j] = tmp;
                }
            }

            while (currentIter++ < imax)
            {
                // aktualnie najlepsze rozwiazanie
                for (int i = 0; i < m; i++)
                {
                    if (Reward(x[i], A, B) < Reward(x[min], A, B))
                    {
                        min = i;
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
                            double beta = Attractiveness(beta0, gamma, dist);
                            //zbliz swietlika xi do xj
                            x[i] = Approach(beta, x[i], x[j]);
                            x[i] = PerformRandomFlight(x[i], alfa);
                        }
                    }
                }

                // aktualnie najlepsze rozw porusza sie losowo

                x[min] = PerformRandomFlight(x[min], alfa);
            }

            Console.WriteLine("Najlepsze rozwiazanie:\n (");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(x[min][i] + ", ");
            }
            Console.WriteLine(")");
        }

        private int[] PerformRandomFlight(int[] xi, double alfa)
        {
            int n = xi.Length;
            var result = new int[n];
            if(alfa < 0 || alfa >= n ) throw new Exception("Zla alfa");
            var rand = new Random();
            
            int nrOfElementsToShuffle = (int) (alfa*rand.NextDouble());
            var indexesLeft = Enumerable.Range(0, n).ToList(); // {0, 1, ..., n-1}
            
            throw new NotImplementedException();

            return result;
        }

        private int[] Approach(double beta, int[] xi, int[] xj)
        {
            var rand = new Random();
            var n = xi.Length;
            var result = new int[n]; //result
            var valuesUsed = new HashSet<int>(); //valsUsed  //values already used used in result
            HashSet<int> valuesLeft = (HashSet<int>) Enumerable.Range(0, n); // {0, 1, ..., n-1} possible values
            var gaps = new List<int>(); //indFree  //indexes of gaps which still need to be filled
            var gapsToRandomize = new List<int>(); //gaps for which there is no rescue

            // najpierw przepisujemy te miejsca ktore sie zgadzaja
            // tworzymy zbior wartosci do obsadzenia i miejsc w ktorych mozna obsadzic wartosci
            for (int i = 0; i < n; i++)
            {
                if (xi[i] == xj[i])
                {
                    result[i] = xi[i];
                    valuesUsed.Add(result[i]);
                    valuesLeft.Remove(result[i]);
                }
                else
                {
                    result[i] = -1;
                    gaps.Add(i);
                }
            }

            // wypelniamy te miejsca gdzie nie ma konfliktu uzytych obu propozycji
            while (gaps.Count > 0)
            {
                int tmp = gaps[rand.Next(gaps.Count)];

                if (!valuesUsed.Contains(xi[tmp]) && !valuesUsed.Contains(xj[tmp]))
                {
                    // wybierz bardziej prawdopodobna 
                    if (rand.NextDouble() < beta)
                    {
                        result[tmp] = xj[tmp];
                    }
                    else
                    {
                        result[tmp] = xi[tmp];
                    }
                    valuesUsed.Add(result[tmp]);
                    valuesLeft.Remove(result[tmp]);
                }
                else if (valuesUsed.Contains(xi[tmp]) && valuesUsed.Contains(xj[tmp]))
                {
                    gapsToRandomize.Add(tmp);
                }
                else if (valuesUsed.Contains(xi[tmp]))
                {
                    result[tmp] = xj[tmp];
                    valuesUsed.Add(xj[tmp]);
                    valuesLeft.Remove(xj[tmp]);
                }
                else if (valuesUsed.Contains(xj[tmp]))
                {
                    result[tmp] = xi[tmp];
                    valuesUsed.Add(xi[tmp]);
                    valuesLeft.Remove(xi[tmp]);
                }


                gaps.Remove(tmp);
            }

            // tu brakuje wypelnienia miejsc gdzie mozliwe wartosci z xi i xj zostaly uzyte
            // losowymi wartosciami o indeksach z indRand

            
            var valuesLeftAsList = valuesLeft.ToList();
            foreach (var gap in gapsToRandomize)
            {
                result[gap] = valuesLeftAsList.ElementAt(rand.Next(valuesLeftAsList.Count));
                valuesLeft.Remove(result[gap]);
            }
            if(!valuesLeftAsList.Any()) throw new Exception("blad : zostaly jakies wartosci");

            return result;
        }

        private double Attractiveness(double beta0, double gamma, int dist)
        {
            //return 1/(1+ gamma*dist*dist)
            return beta0*Math.Exp(-gamma*dist*dist);

        }

        private int Distance(int[] xi, int[] xj)
        {
            var n = xi.Length;
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

        private int Reward(int[] solut, int[][] A, int[][] B)
        {
            int sum = 0;
            int n = A.Length;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sum += A[i][j]*B[solut[i]][solut[j]];
                }
            }

            return sum;
        }
    }
}