using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace Model
{
    public class AntColony : IAlgorithm
    {
        private int _ants; //liczba mrowek
        private int _maxAssigns; //liczba iteracji
        private float _alpha; //wspolczynnik do feromonow
        private float _beta; //wspolczynnik do odleglosci
        private float _rho; //wspolczynnik parowania feromonow
        private float _q; //ilosc pozostawianego feromonu
        private float _q0; //ilosc feromonu powyzej ktorego mrowki zachowuja sie zachlannie
        private float _t0; //poczatkowa wartosc feromonu
        private float _Q; // licznik we wspolczynniku odleglosci
        private BackgroundWorker _backgroundWorker;
        public int[,] A; ///macierz odleglosci
        public int[,] B; //macierz kosztow
        public int n; //wymiar macierzy

        public int CurrentIteration { get; private set; }
        public bool IsInitialized { get; private set; }
        public bool HasFinished { get; private set; }

        private readonly Random rand;
        private int[] bestSolution; //najlepsze rozwiazanie
        private int[][] x; //i-ty wiersz zawiera tablice z permutacja dla i-tej mrowki
        private float[][] pheromones; //ilosc feromonow na poszczegolnych sciezkach
        private int minimum = 0;
        public AntColony(int nr, int iter, float alpha, float beta, float ro, float q, float q0, float t0, float Q)
        {
            rand = new Random();
            _ants = nr; _maxAssigns = iter; _alpha = alpha; _beta = beta; _rho = ro; _q = q; _q0 = q0; _t0 = t0; _Q = Q;
        }

        public int Ants
        {
            set { _ants = value; }
            get { return _ants; }
        }





        public int MaxAssigns
        {
            set { _maxAssigns = value; }
            get { return _maxAssigns; }
        }


        public float Alpha
        {
            set { _alpha = value; }
            get { return _alpha; }
        }


        public float Beta
        {
            set { _beta = value; }
            get { return _beta; }
        }


        public float Rho
        {
            set { _rho = value; }
            get { return _rho; }
        }


        public float q
        {
            set { _q = value; }
            get { return _q; }
        }


        public float Q0
        {
            set { _q0 = value; }
            get { return _q0; }
        }


        public float T0
        {
            set { _t0 = value; }
            get { return _t0; }
        }


        public float Q
        {
            set { _Q = value; }
            get { return _Q; }
        }

        public int GetCurrentCost()
        {
            return Cost(bestSolution, A, B);
        }

        public void addBackgroundWorker(BackgroundWorker worker)
        {
            this._backgroundWorker = worker;
        }

        public void InitializeAlgorithm()
        {
            x = new int[_ants][];
            pheromones = new float[n][];
            for (int i = 0; i < n; i++)
            {
                pheromones[i] = new float[n];
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                        pheromones[i][j] = _t0;
                }
            }
            bestSolution = new int[n];
            for (int i = 0; i < n; i++)
            {
                bestSolution[i] = i;
            }
            //Console.WriteLine(n);
            for (int i = 0; i < _ants; i++)
            {
                x[i] = new int[n];
                for (int j = 0; j < n; j++)
                {
                    int tmp = rand.Next(n);
                    bool used = true;

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


        public List<int> ReturnMinimalResult()
        {
            if (!HasFinished)
                throw new Exception("still counting...");
            List<int> list = new List<int>();
            for (int i = 0; i < n; i++)
            {
                list.Add(bestSolution[i]);
            }
            return list;
        }

        public int GetMinimalCost()
        {
            if (!HasFinished)
                throw new Exception("still counting...");
            return Cost(bestSolution, A, B);
        }

        public List<int> GetCosts(int numberOfIterations)
        {
            throw new NotImplementedException();
        }

        public List<string> GetParameterNames()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, double> GetParameterValues()
        {
            throw new NotImplementedException();
        }

        public void SetParameters(Dictionary<string, double> parameters)
        {
            throw new NotImplementedException();
        }

        public void SetTestData(int[,] A, int[,] B, int numberOfInstances)
        {
            this.A = A;
            this.B = B;
            this.n = numberOfInstances;
        }

        public void RunAlgorithm()
        {
            if (!IsInitialized)
                InitializeAlgorithm();

            /*   for (int i = 0; i < _ants; i++)
               {
                   for (int j = 0; j < n; j++)
                   {
                       Console.Write(x[i][j]);
                   }
                   Console.WriteLine("");
               }
               Console.ReadKey();*/

            CurrentIteration = 0;

            while (CurrentIteration++ < _maxAssigns)
            {
                /* for (int i = 0; i < n; i++)
                 {
                     for (int j = 0; j < n; j++)
                     {
                         if (pheromones[i][j] >50)
                             Console.Write(pheromones[i][j] + " ");
                     }
                     //Console.Write("\n");
                 }*/
                //Console.WriteLine("#########");
                //aktualizacja dla najlepszej mrowki w iteracji
                updatePheromone(x[minimum]);
                //aktualizacja dla najlepszej drogi w ogole
                //updatePheromone(bestSolution);


                for (int i = 0; i < _ants; i++)
                {

                    if (Cost(x[i], A, B) < Cost(x[minimum], A, B))
                    {
                        //Console.WriteLine("test1");
                        minimum = i;
                        if (Cost(x[minimum], A, B) < Cost(bestSolution, A, B))
                        {
                            for (int p = 0; p < n; p++)
                            {
                                bestSolution[p] = x[minimum][p];
                            }
                        }
                    }

                }
                Console.WriteLine(CurrentIteration + ": " + Cost(x[minimum], A, B) + " " + Cost(bestSolution, A, B));
                for (int ant = 0; ant < _ants; ant++)
                {
                    bool[] nodes = new bool[n];
                    for (int i = 0; i < n; i++)
                        nodes[i] = false;

                    int node = rand.Next(0, n - 1); //numer od ktorego zaczynamy przejscie mrowka
                    //Console.WriteLine("node: " + node);
                    nodes[node] = true;
                    x[ant][0] = node;
                    for (int i = 1; i < n; i++)
                    {
                        //wybieramy kolejne wezly sciezki
                        bool greedy = false;
                        int chosenNode = -1; //0!!
                        for (int j = 0; j < n; j++)
                        {
                            //sprawdzamy czy dla kazdego przejscia z danego wezla mamy zachowywac sie zachlannie czy normalnie
                            if (pheromones[node][j] > _q0 && j != node && !nodes[j])
                            {
                                greedy = true;
                                break;
                            }
                        }
                        if (greedy)
                        {
                            //zachowujemy sie zachlannie
                            //Console.WriteLine("greedy");
                            float attr = -1;
                            int ktmp = -1;
                            for (int k = 0; k < n; k++)
                            {
                                if (!nodes[k])
                                {
                                    ktmp = k;
                                    float tmp = GreedyAttractivnes(node, k);
                                    if (tmp > attr)
                                    {
                                        attr = tmp;
                                        chosenNode = k;
                                    }
                                }

                            }
                            if (chosenNode == -1)
                            {
                                chosenNode = ktmp;
                                Console.WriteLine("ktmp: " + ktmp);
                            }
                        }
                        else
                        {
                            //zachowujemy sie normalnie
                            // Console.WriteLine("normal");
                            float choice = (float)rand.NextDouble();
                            float attr = 0;
                            float max = -1;
                            float suma = 0;
                            int maxi = -1;
                            float ala = -1;
                            for (int z = 0; z < n; z++)
                            {
                                if (z != node)
                                {
                                    if (!nodes[z])
                                    {
                                        //Console.WriteLine("sfdasdasdasfasfasfsafasf");
                                        maxi = z;
                                    }
                                    else
                                    {
                                        ala = AttractivnesProbability(node, z, nodes);
                                        suma += ala;
                                        if (attr >= 1)
                                            Console.WriteLine("attr: " + ala + " node: " + node + " z: " + z);
                                    }
                                }
                            }
                            choice = choice * (1.0F - suma);
                            //Console.WriteLine("suma : " + suma);
                            //Console.WriteLine("choice : " + choice);
                            if (maxi == -1)
                                Console.WriteLine("PROBLEM########################################");
                            //Console.WriteLine("alamakota");
                            for (int k = 0; k < n; k++)
                            {
                                if (!nodes[k])
                                {
                                    float tmp = AttractivnesProbability(node, k, nodes);
                                    if (tmp == 1)
                                    {
                                        chosenNode = k;
                                        break;
                                    }
                                    if (tmp > max)
                                    {
                                        maxi = k;
                                        max = tmp;
                                    }
                                    //Console.WriteLine("ALA " + tmp);
                                    if (choice < tmp)
                                    {
                                        chosenNode = k;
                                        break;
                                    }
                                    else
                                    {
                                        //Console.WriteLine(k);
                                        if (tmp > attr)
                                        {
                                            attr = tmp;
                                            chosenNode = k;
                                        }
                                        choice -= tmp;
                                    }
                                }
                            }
                            if (chosenNode == -1)
                            {
                                chosenNode = maxi;
                            }
                        }
                        //aktualizujemy wezly
                        //Console.WriteLine("chosenNode: " + chosenNode);
                        //Console.ReadKey();
                        node = chosenNode;
                        nodes[chosenNode] = true;
                        x[ant][i] = node;

                    }
                    //aktualizacja dla kazdej mrowki
                    //updatePheromone(x[ant]);
                    /* for (int u = 0; u < n; u++)
                         Console.Write(x[ant][u] + " ");
                     Console.Write("\n");*/
                }
                if (cancel) break;
                if (_backgroundWorker != null) _backgroundWorker.ReportProgress(Cost(bestSolution, A, B));
            }
            HasFinished = true;
        }

        private void updatePheromone(int[] solution)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {   //feromony paruja
                    pheromones[i][j] *= (1.0F - _rho);
                }
            }
            for (int i = 0; i < n - 1; i++)
            { //aktualizujemy feromony na najlepszej sciezce
                pheromones[solution[i]][solution[(i + 1) % n]] += _q;
            }
        }

        private float GreedyAttractivnes(int node1, int node2)
        {
            if (A[node1, node2] == 0)
                return 0.99999F;
            //Console.WriteLine("######## greedy pheromones: " + pheromones[node1][node2]);
            return (float)Math.Pow((double)pheromones[node1][node2], (double)_alpha)
                    * (float)Math.Pow((double)_Q / (double)A[node1, node2], (double)_beta);
        }

        private float AttractivnesProbability(int node1, int node2, bool[] nodes)
        {
            if (A[node1, node2] == 0)
                return 1;
            float numerator = (float)Math.Pow((double)pheromones[node1][node2], (double)_alpha)
                    * (float)Math.Pow((double)_Q / (double)A[node1, node2], (double)_beta);

            float denominator = 0;
            for (int i = 0; i < n; i++)
            {
                if (i != node1)
                {
                    if (A[node1, i] == 0)
                        continue;
                    denominator += (float)Math.Pow((double)pheromones[node1][i], (double)_alpha)
                    * (float)Math.Pow((double)_Q / (double)A[node1, i], (double)_beta);
                }
            }
            if (denominator == 0)
            {
                // Console.WriteLine("0");
                return 1.0F;
            }
            //Console.WriteLine("licnzik " + numerator + " mianownik " + denominator);
            return numerator / denominator;
        }

        private int Cost(int[] solution, int[,] A, int[,] B)
        {//wyliczanie kosztu rozwiazania
            int sum = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //Console.WriteLine("#####" + solution[i] + solution[j]);
                    //Console.ReadKey();
                    sum += A[i, j] * B[solution[i], solution[j]];
                }
            }
            return sum;
        }

        private bool cancel;
        public void CancelAlgorithm()
        {
            cancel = true;
        }
    }
}
