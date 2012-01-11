using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Model;

namespace Test
{
    public class FireflyTest
    {
        private int[,] A;
        private int[,] B;
        private int size;


        public void RunTest(string filename)
        {
            LoadData(filename);
            int liczbaSw = 100;
            int liczbaIter = 400;
            double gamma = 1.0;
            int alfa = 2;
            int result;
            int bestResult;
            int bestAlfa;
            double bestGamma;
            result = RunTest(liczbaSw, liczbaIter, gamma, alfa);
            //Console.WriteLine("gamma: {0}, alfa: {1}, result: {2}",gamma, alfa, result );
            Console.WriteLine(gamma.ToString("0.0", CultureInfo.GetCultureInfo("en-US")) + " " + alfa + " " + result); 
            bestResult = result;
            bestAlfa = alfa;
            bestGamma = gamma;
//            for (alfa = 3; alfa <= size; alfa++)
//            {
//                result = RunTest(liczbaSw, liczbaIter, gamma, alfa);
//                Console.WriteLine("gamma: {0}, alfa: {1}, result: {2}", gamma, alfa, result);
//                if(result < bestResult)
//                {
//                    bestResult = result;
//                    bestAlfa = alfa;
//                }
//            }
//            alfa = bestAlfa;
//            for (gamma = 0.1; gamma <= 10; gamma+=0.1)
//            {
//                result = RunTest(liczbaSw, liczbaIter, gamma, alfa);
//                Console.WriteLine("gamma: {0}, alfa: {1}, result: {2}", gamma, alfa, result);
//                if (result < bestResult)
//                {
//                    bestResult = result;
//                    bestGamma = gamma;
//                }
//            }

            for (alfa = 2; alfa <= size; alfa++)
            {
                for (gamma = 0.1; gamma <= 10; gamma += 0.1)
                {
                    result = RunTest(liczbaSw, liczbaIter, gamma, alfa);
                    //Console.WriteLine("gamma: {0}, alfa: {1}, result: {2}", gamma, alfa, result);
                    Console.WriteLine(gamma.ToString("0.0", CultureInfo.GetCultureInfo("en-US")) + " " + alfa + " " + result);
                    if (result < bestResult)
                    {
                        bestResult = result;
                        bestAlfa = alfa;
                        bestGamma = gamma;
                    }
                }
            }

            //Console.WriteLine("Koniec testu, best result: {0} dla alfa = {1}, gamma = {2}",bestResult, bestAlfa, bestGamma);
        }

        public void LoadData(string filename)
        {
            // create reader & open file
            var tr = new StreamReader(filename);

            // read a line of text
            A = null;
            int i = 0;
            int j = 0;
            int m = 0;
            while (!tr.EndOfStream)
            {
                string[] line = tr.ReadLine().Split(' ');
                foreach (string s in line)
                {
                    int a;
                    if (Int32.TryParse(s, out a))
                    {
                        if (A == null)
                        {
                            A = new int[a,a];
                            B = new int[a,a];
                            size = a;
                        }
                        else
                        {
                            if (m == 0) A[j, i] = a;
                            if (m == 1) B[j, i] = a;
                            ++i;
                            if (i == size)
                            {
                                i = 0;
                                ++j;
                                if (j == size)
                                {
                                    i = 0;
                                    j = 0;
                                    ++m;
                                }
                            }
                        }
                    }
                }
            }
            // close the stream
            tr.Close();

            /*FireflyAlgorithm algorithm = new FireflyAlgorithm()
            {
                M = 10, //liczba swietlikow
                Imax = 100, //max liczba iteracji
                Gamma = 1.0, //wsp absorpcji
                Alfa = 2 //waga losowego kroku <= n
            };
//            name = "AlgorytmŚwietlikowy";
//            int iterations = algorithm.Imax;

            algorithm.SetTestData((int[,])A.Clone(), (int[,])B.Clone(), size);
//            var sth = new Chart(algorithm, iterations, name, this.iterationGap, filename);
            algorithm.RunAlgorithm();
            return algorithm.GetMinimalCost();*/
        }

        public int RunTest(int liczbaSwietlikow, int maxLiczbaIteracji,
                           double gamma, int alfa)
        {
            FireflyAlgorithm algorithm = new FireflyAlgorithm()
                                             {
                                                 M = 10,
                                                 //liczba swietlikow
                                                 Imax = 100,
                                                 //max liczba iteracji
                                                 Gamma = 1.0,
                                                 //wsp absorpcji
                                                 Alfa = 2 //waga losowego kroku <= n
                                             };
            algorithm.SetTestData((int[,]) A.Clone(), (int[,]) B.Clone(), size);

            algorithm.RunAlgorithm();
            return algorithm.GetMinimalCost();
        }
    }
}