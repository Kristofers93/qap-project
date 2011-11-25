﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Model;
using Structures;
using System.Windows;

namespace GUI
{
    class ResultViewModel : Structures.IResultViewModel, INotifyPropertyChanged
    {
        private readonly BackgroundWorker worker;
        private int progress;
        private bool working;
        private int cost;
        private IntVector vector;
        private readonly IParameters parameters;

        public int Progress
        {
            get { return progress; }
            private set
            {
                progress = value;
                RaisePropertyChanged("Progress");
            }
        }

        private bool Working
        {
            set
            {
                working = value;
                RaisePropertyChanged("Working");
                RaisePropertyChanged("WorkingVisibility");
                RaisePropertyChanged("FinishedVisibility");
            }
        }

        public Visibility WorkingVisibility
        {
            get
            {
                if (working) return Visibility.Visible;
                return Visibility.Collapsed;
            }
        }

        public Visibility FinishedVisibility
        {
            get
            {
                if (working) return Visibility.Collapsed;
                return Visibility.Visible;
            }
        }

        public int Cost
        {
            get { return cost; }
            private set { 
                cost = value;
                RaisePropertyChanged("Cost");
            }
        }

        public IntVector Vector
        {
            get { return vector; }
            private set {
                vector = value;
                RaisePropertyChanged("Vector");
            }
        }

        public void reportPartialResult(int iteration, int cost)
        {
            //TODO zapamiętywanie wyników i rysowanie z nich wykresów
            Progress = 100 * iteration / parameters.iterations;
            Cost = cost;
        }

        public void reportFinalResult(IntVector vector, int cost)
        {
            //TODO ładne wyświetlanie wyników
            Working = false;
            Vector = vector;
            Cost = cost;
        }

        public ResultViewModel(IAlgorithm algorithm)
        {
            algorithm.runAlgorithm();
            //TODO odbieranie i wyświetlanie wyników
            
            /*worker = new BackgroundWorker();
            this.parameters = parameters;
            if (parameters.GetType().Equals(typeof(Mrowkowy.Parameters)))
            {
                Mrowkowy.Worker mWorker = new Mrowkowy.Worker(this, parameters, A, B);
                worker.DoWork += mWorker.DoWork;
            }
            else if (parameters.GetType().Equals(typeof(Pszczeli.Parameters)))
            {
                Pszczeli.Worker pWorker = new Pszczeli.Worker(this, parameters, A, B);
                worker.DoWork += pWorker.DoWork;
            }
            else if (parameters.GetType().Equals(typeof(Swietlikowy.Parameters)))
            {
                Swietlikowy.Worker sWorker = new Swietlikowy.Worker(this, parameters, A, B);
                worker.DoWork += sWorker.DoWork;
            }
            worker.RunWorkerAsync();
            Working = true;*/
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}