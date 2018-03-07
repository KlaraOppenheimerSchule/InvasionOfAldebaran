using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InvasionOfAldebaran.Shared
{
    class RelayCommand : ICommand
    {
        private Action RelayAction;

        public RelayCommand(Action action)
        {
            RelayAction = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            RelayAction.Invoke();
        }
    }
}
