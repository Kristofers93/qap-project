using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Structures;

namespace Swietlikowy
{
    public class Worker
    {
        private IResultViewModel vm;
        private Parameters parameters;
        private IntMatrix2D A, B;

        public Worker(IResultViewModel vm, IParameters parameters, IntMatrix2D A, IntMatrix2D B)
        {
            this.vm = vm;
            this.parameters = parameters as Parameters;
            this.A = A;
            this.B = B;
        }

        public void DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int cost = 999999999; //TODO zmienić na co chcecie
            for (int i = 0; i < parameters.iterations; i++)
            {
                cost--; System.Threading.Thread.Sleep(10); //TODO zamiast tego wstawić obliczenia

                if (i % parameters.reportEveryIterations == 0) vm.reportPartialResult(i + 1, cost);
            }
            vm.reportFinalResult(new IntVector(A.size), cost);
        }
    }
}
