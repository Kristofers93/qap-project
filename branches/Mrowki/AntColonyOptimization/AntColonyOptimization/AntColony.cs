﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntColonyOptimization
{
    class AntColony : IAlgorithm
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

        public int[,] A; ///macierz odleglosci
        public int[,] B; //macierz kosztow
        public int N; //wymiar macierzy

        public int CurrentIteration { get; private set; }
        public bool IsInitialized { get; private set; }
        public bool HasFinished { get; private set; }

        private readonly Random _rand;
        private int[] _costs; //tablica kosztów po wszystkich iteracjach
        private int[] _bestSolution; //najlepsze rozwiazanie
        private int[][] _x; //i-ty wiersz zawiera tablice z permutacja dla i-tej mrowki
        private float[][] _pheromones; //ilosc feromonow na poszczegolnych sciezkach
        private int _minimum = 0;
        public AntColony(int nr, int iter, float alpha, float beta, float ro, float q, float q0, float t0, float Q)
        {
            _rand = new Random();
            _ants = nr; 
            _maxAssigns = iter; 
            _alpha = alpha; 
            _beta = beta; 
            _rho = ro; 
            _q = q; 
            _q0 = q0; 
            _t0 = t0; 
            _Q = Q;
        }

        public void InitializeAlgorithm()
        {
            _x = new int[_ants][];
            _pheromones = new float[N][];
            for (int i = 0; i < N; i++)
            {
                _pheromones[i] = new float[N];
                for (int j = 0; j < N; j++)
                {
                    if (j!=i)
                        _pheromones[i][j] = _t0;
                }
            }

            _costs = new int[_maxAssigns];
            for (int i = 0; i < _maxAssigns; i++ )
            {
                _costs[i] = 0;
            }

            _bestSolution = new int[N];
            for (int i = 0; i < N; i++)
            {
                _bestSolution[i] = i;
            }
            //Console.WriteLine(n);
            for (int i = 0; i < _ants; i++)
            {
                _x[i] = new int[N];
                for (int j = 0; j < N; j++)
                {
                    int tmp = _rand.Next(N);
                    bool used = true;
                    
                    while (used)
                    {
                        bool again = false;
                        
                        for (int k = 0; k < j; k++)
                        {
                            if (_x[i][k] == tmp)
                            {
                                again = true;
                            }
                        }
                        if (again)
                        {
                            tmp = _rand.Next(N);
                        }
                        else
                        {
                            used = false;
                        }
                    }
                    _x[i][j] = tmp;
                }
            }
            IsInitialized = true;
        }


        public List<int> ReturnMinimalResult()
        {
            if (!HasFinished)
                throw new Exception("still counting...");
            var list = new List<int>();
            for (int i=0; i<N; i++)
            {
                list.Add(_bestSolution[i]);
            }
            return list;
        }

        public int GetMinimalCost()
        {
            if (!HasFinished)
                throw new Exception("still counting...");
            return Cost(_bestSolution,A,B);
        }

        public List<int> GetCosts(int numberOfIterations)
        {
            if (!HasFinished)
                throw new Exception("still counting...");
            var result = new int[numberOfIterations];
            for (int i = 0; i < numberOfIterations; i++)
            {
                result[i] = _costs[i];
            }
            return new List<int>(result);
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
            this.N = numberOfInstances;
        }

        public void runAlgorithm()
        {
            if (!IsInitialized)
                InitializeAlgorithm();
            
            /*
            for (int i = 0; i < _ants; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(x[i][j]);
                }
                Console.WriteLine("");
            }
            Console.ReadKey();
             */

            CurrentIteration = 0;
            
            while (CurrentIteration++ < _maxAssigns)
            {
                //aktualizacja dla najlepszej mrowki w iteracji
                UpdatePheromone(_x[_minimum]);
                //aktualizacja dla najlepszej drogi w ogole
                UpdatePheromone(_bestSolution);
                _costs[CurrentIteration - 1] = Cost(_x[_minimum], A, B);
                Console.WriteLine(CurrentIteration + ": " + Cost(_x[_minimum], A, B) + " " + Cost(_bestSolution,A,B));
                for (int i = 0; i < _ants; i++)
                {
                    if (Cost(_x[i], A, B) < Cost(_x[_minimum], A, B))
                    {
                        _minimum = i;
                        if (Cost(_x[_minimum], A, B) < Cost(_bestSolution, A, B))
                        {
                            for (int p = 0; p < N; p++)
                            {
                                _bestSolution[p] = _x[_minimum][p];
                            }
                        }
                    }
                }

                for (int ant = 0; ant < _ants; ant++)
                {
                    var nodes = new bool[N];
                    for (int i = 0; i < N; i++)
                        nodes[i] = false;

                    int node = 0; //numer od ktorego zaczynamy przejscie mrowka
                    nodes[node] = true;
                    _x[ant][0] = node;
                    for (int i = 1; i < N; i++)
                    {//wybieramy kolejne wezly sciezki
                        bool greedy = true;
                        int chosenNode = 0;
                        for (int j = 0; j < N; j++)
                        { //sprawdzamy czy dla kazdego przejscia z danego wezla mamy zachowywac sie zachlannie czy normalnie
                            if (_pheromones[node][j] < _q0)
                            {
                                greedy = false;
                                break;
                            }
                        }
                        if (greedy)
                        {//zachowujemy sie zachlannie
                            //Console.WriteLine("greedy");
                            float attr = 0;
                            for (int k = i; k < N; k++)
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
                            //Console.WriteLine("normal");
                            float choice = (float)_rand.NextDouble();
                            float attr = 0;
                            //Console.WriteLine("alamakota");
                            for (int k = 1; k < N; k++)
                            {
                                if (!nodes[k])
                                {
                                    float tmp = AttractivnesProbability(node, k,nodes);
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
                        }
                        //aktualizujemy wezly
                        //Console.WriteLine("chosenNode: " + chosenNode);
                        //Console.ReadKey();
                        node = chosenNode;
                        nodes[chosenNode] = true;
                        _x[ant][i] = node;

                    }
                    //aktualizacja dla kazdej mrowki
                    
                    UpdatePheromone(_x[ant]);
                    
                }
                
            }
            HasFinished = true;
        }

        private void UpdatePheromone(int[] solution)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {   //feromony paruja
                    _pheromones[i][j] *= _rho;
                }
            }
            for (int i = 0; i < N; i++)
            { //aktualizujemy feromony na najlepszej sciezce
                _pheromones[solution[i]][solution[(i + 1) % N]] += _q;
            }
        }

        private float GreedyAttractivnes(int node1, int node2)
        {
            return (float)Math.Pow((double)_pheromones[node1][node2],(double)_alpha)
                    *(float)Math.Pow((double)_Q/(double)A[node1,node2],(double)_beta);
        }

        private float AttractivnesProbability(int node1, int node2, bool[] nodes)
        {
            float numerator = (float)Math.Pow((double)_pheromones[node1][node2], (double)_alpha)
                    * (float)Math.Pow((double)_Q / (double)A[node1, node2], (double)_beta);
            float denominator = 0;
            for (int i = 1; i<N; i++)
            {
                if (!nodes[i] && i!=node1)
                {
                    denominator += (float)Math.Pow((double)_pheromones[node1][i], (double)_alpha)
                    * (float)Math.Pow((double)_Q / (double)A[node1, i], (double)_beta);
                }
            }
            //Console.WriteLine("licnzik " + numerator + " mianownik " + denominator);
            return numerator / denominator;
        }

        private int Cost(int[] solution, int[,] A, int[,] B)
        {//wyliczanie kosztu rozwiazania
            int sum = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    //Console.WriteLine("#####" + solution[i] + solution[j]);
                    //Console.ReadKey();
                    sum += A[i, j] * B[solution[i], solution[j]];
                }
            }
            return sum;
        }
    }
}
