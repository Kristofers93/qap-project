using System;
using System.Windows.Input;

namespace GUI
{
    public class RelayCommand : ICommand
    {
        private readonly Action _action;

        public RelayCommand(Action execute)
        {
            _action = execute;
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }

        #endregion
    }
}