using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace GUI
{
    class MainWindowViewModel
    {
        public void LoadData()
        {
            //TODO
            System.Windows.MessageBox.Show("Nie zaimplementowano :P");
        }

        public void Run()
        {
            //TODO
            System.Windows.MessageBox.Show("Nie zaimplementowano :P");
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
