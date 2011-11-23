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
        // void - wypiszmy wynik na ekran
        public void TmpRun(int imax, int m, double beta0, double gamma, double alfa, int [][]A, int [][]B)
        {
            int n = A.Length; //wymiar macierzy, dlugosc permutacji
            Random rand = new Random();
            int currentIter = 0;
            int min = 0;
            int[][] x = new int[m][];
            
            // losowanie wejsciowych permutacji
            for (int i=0; i<m; i++)
            {
                x[i] = new int[n];
                for (int j=0; j<n; j++)
                {
                    int tmp;
                    bool used = true;
                    tmp = rand.Next(n);
                    while (used)
                    {
                        for (int k=0; k<j; k++)
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
                for (int i=0; i<m; i++)
                {
                    if (Reward(x[i],A,B) < Reward(x[min],A,B))
                    {
                        min = i;
                    }
                }

                // zblizanie swietlikow do mocniej swiecacych
                for (int i=0; i<m; i++)
                {
                    for (int j=0; j<m; j++)
                    {
                        if (Reward(x[i],A,B)<Reward(x[j],A,B))
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
            throw new NotImplementedException();
        }

        private int[] Approach(double beta, int[] xi, int[] xj)
        {
            var rand = new Random();
            var n = xi.Length;
            var r = new int[n]; //result
            var valsUsed = new HashSet<int>();
            var indFree = new List<int>();
            var indRand = new List<int>();

            // najpierw przepisujemy te miejsca ktore sie zgadzaja
            // tworzymy zbior wartosci do obsadzenia i miejsc w ktorych mozna obsadzic wartosci
            for (int i = 0; i < n; i++ )
            {
                
                if (xi[i] == xj[i])
                {
                    r[i] = xi[i];
                    valsUsed.Add(r[i]);
                }
                else
                {
                    r[i] = -1;
                    indFree.Add(i);
                }
            }

            // wypelniamy te miejsca gdzie nie ma konfliktu uzytych obu propozycji
            while (indFree.Count > 0)
            {
                int tmp = indFree[rand.Next(indFree.Count)];
                if (!valsUsed.Contains(xi[tmp]) && !valsUsed.Contains(xj[tmp]))
                {
                    // wybierz bardziej prawdopodobna 
                    if (rand.NextDouble() < beta)
                    {
                        r[tmp] = xj[tmp];
                    }
                    else
                    {
                        r[tmp] = xi[tmp];
                    }
                    valsUsed.Add(r[tmp]);
                }
                else if (valsUsed.Contains(xi[tmp]) && valsUsed.Contains(xj[tmp]))
                {
                    indRand.Add(tmp);
                }
                else if (valsUsed.Contains(xi[tmp]))
                {
                    r[tmp] = xj[tmp];
                    valsUsed.Add(xj[tmp]);
                }
                else if (valsUsed.Contains(xj[tmp]))
                {
                    r[tmp] = xi[tmp];
                    valsUsed.Add(xi[tmp]);
                }
                
                ////
                /// tu brakuje wypelnienia miejsc gdzie mozliwe wartosci z xi i xj zostaly uzyte
                /// losowymi wartosciami o indeksach z indRand
                /// 


                indFree.Remove(tmp);
            }

            throw new NotImplementedException();

            return r;
        }

        private double Attractiveness(double beta0, double gamma, int dist)
        {
            return beta0*Math.Exp(-gamma*dist*dist);
        }

        private int Distance(int[] xi, int[] xj)
        {
            var n = xi.Length;
            var d = 0;
            for (var k=0; k<n; k++)
            {
                if (xi[k] != xj[k])
                {
                    d += 1;
                }
            }
            return d;
        }

        private int Reward(int[] solut, int[][]A, int [][]B)
        {
            int sum = 0;
            int n = A.Length;

            for (int i = 0; i < n; i++)
            {
                for (int j=0; j<n; j++)
                {
                    sum += A[i][j]*B[solut[i]][solut[j]];
                }    
            }

            return sum;
        }
    }
}
