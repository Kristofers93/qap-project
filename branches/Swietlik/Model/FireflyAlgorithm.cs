using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class FireflyAlgorithm : IAlgorithm
    {
        //lista reprezentujaca najbardziej optymalne rozwiazanie
        public IList<int> ReturnMinimalResult()
        {
            throw new NotImplementedException();
        }

        //najmniejszy zwracany koszt
        public int GetMinimalCost()
        {
            throw new NotImplementedException();
        }

        //koszt dla danej iteracji
        public int GetCurrentCost(int iterationNumber)
        {
            throw new NotImplementedException();
        }

        //zwraca liste parametrow
        public List<string> GetParameterNames()
        {
            throw new NotImplementedException();
        }

        //zwraca słownik zawierający wszystkie wartości domyślne parametrów
        public Dictionary<string, double> GetParameterValues()
        {
            throw new NotImplementedException();
        }

        //ustawianie niedomyślnych parametrów, klucz - nazwa,
        public void SetParameters(Dictionary<string, double> parameters)
        {
            throw new NotImplementedException();
        }

        //ustawianie danych, macierze A i B z modelu i ich rozmiar,
        public void SetTestData(int[,] A, int[,] B, int numberOfInstances)
        {
            throw new NotImplementedException();
        }

        //uruchomienie obliczen, zwraca 0 w przypadku konca obliczen?
        public int RunAlgorithm()
        {
            return 0;
        }
    }
}
