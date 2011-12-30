using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Model;
using System.ComponentModel;
namespace GUI
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private int[,] A;
        private int[,] B;
        private int size;
        private string filename="brak wczytanego pliku";
        public event PropertyChangedEventHandler PropertyChanged;

        //do przechowywania danych o algorytmie świetlikowym
        private FireflyAlgorithm fireflyAlgorithm = new FireflyAlgorithm()
        {
            M = 10,
            Imax = 100,
            Gamma = 1.0,
            Alfa =2
        };

        //do przechowywania danych o algorytmie pszczelim
        private BeesAlgorithm beeAlgorithm = new BeesAlgorithm()
        {
            Nb=10,
            M=10,
            Imax=100,
            E=3,
            Nep=40,
            Nsp=20,
            Ngh=0.2

        };

        //do przechowywania danych o algorytmie mrówkowym
        public AntColony _antColony = new AntColony(10, 100, (float)2.0, (float)2.0, (float)0.5, (float)3.0, (float)1.0, (float)1.0, (float)5.0);
        public BeeAlgorithm beeAlgorithmSBC= new BeeAlgorithm(10, 5,10, 100, 0.9, 0.1);

        public FireflyAlgorithm FireflyAlgorithm
        {
            get { return fireflyAlgorithm; }
            set { fireflyAlgorithm = value; }
        }

        public BeesAlgorithm BeeAlgorithm
        {
            get { return beeAlgorithm; }
            set { beeAlgorithm = value; }
        }

        public BeeAlgorithm BeeAlgorithmSBC
        {
            get { return beeAlgorithmSBC; }
            set { beeAlgorithmSBC = value; }
        }

        public AntColony antColony
        {
            get { return _antColony; }
            set { _antColony = value; }
        }

        //co jaki czas raportowanie
        int _iterationGap=1;
        public int iterationGap
        {
            get { return _iterationGap; }
            set { _iterationGap = value; }
        }

        public string Filename {
            get { return filename; }
            set
            {
                 if (value != this.filename)
                {
                    this.filename = value;
                    RaisePropertyChanged("Filename");
                }
            }
        }
        public int SelectedTab { get; set; }

        
   

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
                Filename = dlg.FileName;

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
            IAlgorithm algorithm = null;
            string name="Algorytm";
            int iterations=0;

            //wybieranie algorytmu
            switch (SelectedTab)
            {
                case 0:
                    algorithm = new AntColony(_antColony.Ants,_antColony.MaxAssigns,_antColony.Alpha,_antColony.Beta,_antColony.Rho,_antColony.q,_antColony.Q0,_antColony.T0,_antColony.Q);
                    name = "AlgorytmMrówkowy";
                    iterations=_antColony.MaxAssigns;
                    
                    break;
                case 1:
                    algorithm = new BeesAlgorithm()
                    {
                        M = BeeAlgorithm.M,
                        Imax = BeeAlgorithm.Imax,
                        E = beeAlgorithm.E,
                        Ngh=beeAlgorithm.Ngh,
                        Nsp=beeAlgorithm.Nsp,
                        Nb=beeAlgorithm.Nb,
                        Nep=beeAlgorithm.Nep
                    };
                    name = "AlgorytmPszczeli";
                    iterations= BeeAlgorithm.Imax;
                    break;

                case 2:
                    algorithm=new FireflyAlgorithm()
                        {
                            M = FireflyAlgorithm.M,
                            Imax = FireflyAlgorithm.Imax,
                            Gamma = FireflyAlgorithm.Gamma,
                            Alfa = FireflyAlgorithm.Alfa
                        };
                    name = "AlgorytmŚwietlikowy";
                    iterations=FireflyAlgorithm.Imax;
                    break;
                case 3:
                    algorithm = new BeeAlgorithm(this.beeAlgorithmSBC.TotalNumberBees, this.beeAlgorithmSBC.NumberScout, this.beeAlgorithmSBC.MaxNumberVisits, this.beeAlgorithmSBC.MaxNumberCycles, this.beeAlgorithmSBC.ProbPersuasion, this.beeAlgorithmSBC.ProbMistake);
                    name = "AlgorytmPszczeliBSC";
                    iterations = FireflyAlgorithm.Imax;
                    break;
            }

            if (algorithm == null)
            {
                MessageBox.Show("Nie załadowano algorytmu.");
                return;
            }
             
            //ładowanie algorytmu
            algorithm.SetTestData((int[,]) A.Clone(), (int[,]) B.Clone(), size);
            var sth = new Chart(algorithm,iterations,name,this.iterationGap,filename);
            sth.Show();
        }

        public virtual void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));

        }
    }
}