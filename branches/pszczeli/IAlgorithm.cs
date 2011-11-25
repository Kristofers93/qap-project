using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Pszczeli
{
    interface IAlgorithm
    {
        //lista reprezentujaca najbardziej optymalne rozwiazanie
        List<int> ReturnMinimalResult();

        //najmniejszy zwracany koszt
        int GetMinimalCost();

        //koszt dla pierwszych n(numberOfIterations) iteracji
        List<int> GetCosts(int numberOfIterations);

        //zwraca liste parametrow
        List<string> GetParameterNames();

        //zwraca słownik zawierający wszystkie wartości domyślne parametrów
        Dictionary<string, double> GetParameterValues();

        //ustawianie niedomyślnych parametrów, klucz - nazwa,
        void SetParameters(Dictionary<string, double> parameters);


        //ustawianie danych, macierze A i B z modelu i ich rozmiar,
        void SetTestData(int[,] A, int[,] B, int numberOfInstances);



        //uruchomienie obliczen
        void runAlgorithm();
    }
}