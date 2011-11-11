using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using Structures;

namespace GUI
{
    class MainWindowViewModel
    {

        private Mrowkowy.Parameters mrowkowyParameters = new Mrowkowy.Parameters();
        private Pszczeli.Parameters pszczeliParameters = new Pszczeli.Parameters();
        private Swietlikowy.Parameters swietlikowyParameters = new Swietlikowy.Parameters();

        private IntMatrix2D A = new IntMatrix2D(10); //TODO Wczytywanie z pliku
        private IntMatrix2D B = new IntMatrix2D(10); //TODO ^^

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
            //TODO
            //A = ...
            //B = ...
            System.Windows.MessageBox.Show("Nie zaimplementowano :P");
        }

        public void Run()
        {
            switch (SelectedTab)
            {
                case 0:
                    new Result(MrowkowyParameters, A, B).Show();
                    break;
                case 1:
                    new Result(PszczeliParameters, A, B).Show();
                    break;
                case 2:
                    new Result(SwietlikowyParameters, A, B).Show();
                    break;
            }
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
