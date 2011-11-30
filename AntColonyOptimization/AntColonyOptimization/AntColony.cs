using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntColonyOptimization
{
    class AntColony : IAlgorithm
    {
        private int _ants; //number of ants
        private int _maxAssigns; //max number of iterations
        private float _alpha; //emphasis of pheromone
        private float _beta; //emphasis of heuristic info (distance)
        private float _rho; //pheromone evaporation parameter
        private float _q; //pheromone deposit factor
        private float _q0; //pheromone concentration above which the ants operate greedily
        private float _t0; //pheromone concentration on start
        private float _Q;

        public int[,] A;
        public int[,] B;
        public int n;

        public int CurrentIteration { get; private set; }
        public bool IsInitialized { get; private set; }
        public bool HasFinished { get; private set; }

        private readonly Random rand;
        private int[][] x;
        private float[][] pheromones;
        private int minimum = 0;
        public AntColony()
        {
            rand = new Random();
        }

        public void InitializeAlgorithm()
        {
            x = new int[_ants][];
            pheromones = new float[n][];
            for (int i = 0; i < _ants; i++)
            {
                x[i] = new int[n];
                pheromones[i] = new float[n];
                for (int j = 0; j < n; j++)
                {
                    int tmp = rand.Next(n);
                    bool used = true;
                    pheromones[i][j] = _t0;
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
            for (int i=0; i<n; i++)
            {
                list.Add(x[minimum][i]);
            }
            return list;
        }

        public int GetMinimalCost()
        {
            if (!HasFinished)
                throw new Exception("still counting...");
            return minimum;
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

        public void runAlgorithm()
        {
            if (!IsInitialized)
                InitializeAlgorithm();

            CurrentIteration = 0;

            while (CurrentIteration++ < _maxAssigns)
            {
                for (int i = 0; i < _ants; i++)
                {
                    if (Cost(x[i], A, B) < Cost(x[minimum], A, B))
                    {
                        minimum = i;
                    }

                }
                for (int ant = 0; ant < _ants; ant++)
                {
                    bool[] nodes = new bool[n];
                    for (int i = 0; i < n; i++)
                        nodes[i] = false;

                    int node = 0; //numer od ktorego zaczynamy przejscie mrowka
                    nodes[node] = true;
                    x[ant][0] = node;
                    for (int i = 1; i < n; i++)
                    {//wybieramy kolejne wezly sciezki
                        bool greedy = true;
                        int chosenNode=-1;
                        for (int j = 0; j < n; j++)
                        { //sprawdzamy czy dla kazdego przejscia z danego wezla mamy zachowywac sie zachlannie czy normalnie
                            if (pheromones[node][j] < _q0)
                            {
                                greedy = false;
                                break;
                            }
                        }
                        if (greedy)
                        {//zachowujemy sie zachlannie
                            float attr = 0;
                            for (int k = i; k < n; k++)
                            {
                                if (!nodes[k])
                                {
                                    float tmp = GreedyAttractivnes(node, k);
                                    if (tmp > attr)
                                    {
                                        attr = tmp;
                                        chosenNode = k;
                                    }
                                }

                            }
                        }
                        else
                        {//zachowujemy sie normalnie
                            float choice = (float)rand.NextDouble();
                            float attr = 0;
                            for (int k = i; k < n; k++)
                            {
                                if (!nodes[k])
                                {
                                    float tmp = AttractivnesProbability(node, k,nodes);
                                    if (choice < tmp)
                                    {
                                        chosenNode = k;
                                        break;
                                    }
                                    else
                                    {
                                        attr = tmp;
                                        choice -= tmp;
                                    }
                                }
                            }
                        }
                        //aktualizujemy wezly
                        node = chosenNode;
                        nodes[chosenNode] = true;
                        x[ant][i] = node;

                    }
                }
            }
        }

        private void updatePheromone(int[] solution)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {   //feromony paruja
                    pheromones[i][j] *= _rho;
                }
            }
            for (int i = 0; i < n; i++)
            { //aktualizujemy feromony na najlepszej sciezce
                pheromones[i][(i + 1) % n] += _q;
            }


        }

        private float GreedyAttractivnes(int node1, int node2)
        {
            return (float)Math.Pow((double)pheromones[node1][node2],(double)_alpha)
                    *(float)Math.Pow((double)_Q/(double)B[node1,node2],(double)_beta);
        }

        private float AttractivnesProbability(int node1, int node2,bool[] nodes)
        {
            float numerator = (float)Math.Pow((double)pheromones[node1][node2], (double)_alpha)
                    * (float)Math.Pow((double)_Q / (double)B[node1, node2], (double)_beta);
            float denominator = 0;
            for (int i = 1; i<n; i++)
            {
                if (!nodes[i] && i!=node1)
                {
                    denominator += (float)Math.Pow((double)pheromones[node1][i], (double)_alpha)
                    * (float)Math.Pow((double)_Q / (double)B[node1, i], (double)_beta);
                }
            }
            return numerator / denominator;
        }

        private int Cost(int[] solution, int[,] A, int[,] B)
        {//wyliczanie kosztu rozwiazania
            int sum = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sum += A[i, j] * B[solution[i], solution[j]];
                }
            }
            return sum;
        }
    }
}
