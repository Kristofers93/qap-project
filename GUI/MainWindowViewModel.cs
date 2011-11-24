using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using Microsoft.Win32;
using Structures;

namespace GUI
{
    class MainWindowViewModel
    {

        private Mrowkowy.Parameters mrowkowyParameters = new Mrowkowy.Parameters();
        private Pszczeli.Parameters pszczeliParameters = new Pszczeli.Parameters();
        private Swietlikowy.Parameters swietlikowyParameters = new Swietlikowy.Parameters();

        private int[,] A;
        private int[,] B;
        int size = 0;

        public int SelectedTab { get; set; }

        public Mrowkowy.Parameters MrowkowyParameters
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

        public void LoadData()
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //dlg.FileName = "Dane testowe"; // Default file name
            dlg.DefaultExt = ".dat"; // Default file extension
            dlg.Filter = "Dane wejściowe|*.dat"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                
                // create reader & open file
                StreamReader tr = new StreamReader(filename);

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
                        try
                        {
                            int a = Int32.Parse(s);
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

                        }catch
                        {
                            
                        }

                    }
                }
                // close the stream
                tr.Close();
                
            }

           
        }

        public void Run()
        {
            Model.IAlgorithm algorithm = null; //TODO
             switch (SelectedTab)
            {
                case 0:
                    //algorithm = new Mrowkowy();
                    break;
                case 1:
                    //algorithm = new Pszczeli();
                    break;
                case 2:
                    //algorithm = new Swietlikowy();
                    break;
            }          
            if(algorithm==null)
            {
                System.Windows.MessageBox.Show("Nie załadowano algorytmu");
                return;
            }

            algorithm.SetTestData(A, B, size);
            //algorithm.SetParameters();
            new Result(algorithm);

        }

        public ICommand LoadDataCommand
        {
            get { return new RelayCommand(LoadData); }
        }

        public ICommand RunCommand
        {
            get { return new RelayCommand(Run); }
        }

    }
}
