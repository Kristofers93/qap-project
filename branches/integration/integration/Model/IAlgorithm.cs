using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public interface IAlgorithm
    {
        //lista reprezentujaca najbardziej optymalne rozwiazanie
        List<int> ReturnMinimalResult();

        //najmniejszy zwracany koszt
        int GetMinimalCost();
        
        //zwraca liste parametrow
        List<string> GetParameterNames();

        //zwraca słownik zawierający wszystkie wartości domyślne parametrów
        Dictionary<string, double> GetParameterValues();

        int GetCurrentCost();


        //ustawianie danych, macierze A i B z modelu i ich rozmiar,
        void SetTestData(int[,] A, int[,] B, int numberOfInstances);

        //uruchomienie obliczen
        void RunAlgorithm();
    }
}
