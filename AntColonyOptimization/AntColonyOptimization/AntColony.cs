using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntColonyOptimization
{
    class AntColony : IAlgorithm
    {
        private int _ants;
        private int _maxAssigns;
        private float _alpha; //emphasis of pheromone
        private float _beta; //emphasis of heuristic info (distance)
        private float _rho; //pheromone evaporation parameter
        private float _q; //pheromone deposit factor
        private float _q0; //pheromone concentration above which the ants operate greedily
        private float _t0; //pheromone concentration on start



        public List<int> ReturnMinimalResult()
        {
            throw new NotImplementedException();
        }

        public int GetMinimalCost()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void runAlgorithm()
        {
            throw new NotImplementedException();
        }
    }
}
