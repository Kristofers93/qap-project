﻿using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Model;
using Mrowkowy;

namespace GUI
{
    internal class MainWindowViewModel
    {
        private int[,] A;
        private int[,] B;
        private Parameters mrowkowyParameters = new Parameters();
        private Pszczeli.Parameters pszczeliParameters = new Pszczeli.Parameters();
        private int size;
        private Swietlikowy.Parameters swietlikowyParameters = new Swietlikowy.Parameters();

        public int SelectedTab { get; set; }

        public Parameters MrowkowyParameters
        {
            get { return mrowkowyParameters; }
            set { mrowkowyParameters = value; }
        }

        public Pszczeli.Parameters PszczeliParameters
        {
            get { return pszczeliParameters; }
            set { pszczeliParameters = value; }
        }

        public Swietlikowy.Parameters SwietlikowyParameters
        {
            get { return swietlikowyParameters; }
            set { swietlikowyParameters = value; }
        }


        public ICommand LoadDataCommand
        {
            get { return new RelayCommand(LoadData); }
        }

        public ICommand RunCommand
        {
            get { return new RelayCommand(Run); }
        }

        public void LoadData()
        {
            // Configure open file dialog box
            var dlg = new OpenFileDialog();
            //dlg.FileName = "Dane testowe"; // Default file name
            dlg.DefaultExt = ".dat"; // Default file extension
            dlg.Filter = "Dane wejściowe|*.dat"; // Filter files by extension

            // Show open file dialog box
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;

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
            }
        }

        public void Run()
        {
            if (A == null)
            {
                MessageBox.Show("Załaduj najpierw dane");
                return;
            }
            IAlgorithm algorithm = null; //TODO
            switch (SelectedTab)
            {
                case 0:
                    Parameters p = mrowkowyParameters;
                    algorithm = new AntColony(p.insects, p.iterations, p.alpha, p.beta, p.rho, p.q, p.q0, p.t0, p.Q);
                    MessageBox.Show(
                        "Wykres zawiera przykładowe dane z powodu braku implementacji przekazywania częściowych danych w algorytmie!");
                    break;
                case 1:
                    //algorithm = new Pszczeli();
                    break;
                case 2:
                    //algorithm = new Swietlikowy();
                    break;
            }
            if (algorithm == null)
            {
                MessageBox.Show("Nie załadowano algorytmu. Integratorze, do dzieła!");
                return;
            }

            algorithm.SetTestData((int[,]) A.Clone(), (int[,]) B.Clone(), size);
            //algorithm.SetParameters();
            var sth = new Chart(algorithm);
            sth.Show();
        }
    }
}