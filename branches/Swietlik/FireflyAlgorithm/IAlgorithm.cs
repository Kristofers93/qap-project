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
        IList<int> ReturnMinimalResult();

        //najmniejszy zwracany koszt
        int GetMinimalCost();

        //koszt dla danej iteracji
        int GetCurrentCost(int iterationNumber);

        //zwraca liste parametrow
        List<string> GetParameterNames();

        //zwraca słownik zawierający wszystkie wartości domyślne parametrów
        Dictionary<string, double> GetParameterValues();

        //ustawianie niedomyślnych parametrów, klucz - nazwa,
        void SetParameters(Dictionary<string, double> parameters);

        //ustawianie danych, macierze A i B z modelu i ich rozmiar,
        void SetTestData(int[,] A, int[,] B, int numberOfInstances);

        //uruchomienie obliczen, zwraca 0 w przypadku konca obliczen?
        int RunAlgorithm();
    }

}
