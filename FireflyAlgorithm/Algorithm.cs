using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public abstract class Algorithm
    {
        public List<Parameter> Parameters { get; set; }

        //lista reprezentujaca najbardziej optymalne rozwiazanie
        abstract public List<int> ReturnMinimalResult();

        //najmniejszy zwracany koszt
        abstract public int GetMinimalCost();

        //koszt dla pierwszych n(numberOfIterations) iteracji
        abstract public List<int> GetCosts(int numberOfIterations);

        //zwraca liste parametrow
        abstract public List<string> GetParameterNames();

        //zwraca słownik zawierający wszystkie wartości domyślne parametrów
        abstract public Dictionary<string, double> GetParameterValues();

        //ustawianie niedomyślnych parametrów, klucz - nazwa,
        abstract public void SetParameters(Dictionary<string, double> parameters);


        //ustawianie danych, macierze A i B z modelu i ich rozmiar,
        abstract public void SetTestData(int[,] A, int[,] B, int numberOfInstances);

        //uruchomienie obliczen
        abstract public void runAlgorithm();
    }
}
