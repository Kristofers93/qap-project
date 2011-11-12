using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Model
{
    interface IAlgorithm
    {
        //lista rozwiazan, czyli najbardziej optymalne rozwiazanie
        public List<int> ReturnOptimalResult();

        //najmniejszy zwracany koszt
        public int getMinimalCost();

        //koszt dla danej iteracji
        public int getCurrentCost(int iterationNumber);

        //zwraca liste parametrów
        public List<string> getParameterNames();

        //zwraca słownik zawierający wszystkie wartości domyślne parametrów
        public Dictionary<string, double> getParameterValues();
    }
}
