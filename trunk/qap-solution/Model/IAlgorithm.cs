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
        //lista reprezentujaca najbardziej optymalne rozwiazanie
        public List<int> ReturnMinimalResult();

        //najmniejszy zwracany koszt
        public int GetMinimalCost();

        //koszt dla danej iteracji
        public int GetCurrentCost(int iterationNumber);

        //zwraca liste parametrow
        public static List<string> GetParameterNames();

        //zwraca słownik zawierający wszystkie wartości domyślne parametrów
        public static Dictionary<string, double> GetParameterValues();

        //ustawianie niedomyślnych parametrów, klucz - nazwa,
        public void SetParameters(Dictionary<string, double> parameters);


        //ustawianie niedomyślnych parametrów, klucz - nazwa,
        public void SetTestData(int[,] A, int[,] B, int numberOfInstances);

        //uruchomienie obliczen, zwraca 0 w przypadku konca obliczen?
        public int runAlgorithm();
    }

}
