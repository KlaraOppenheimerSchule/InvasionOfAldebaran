using System;
using System.Windows.Input;

namespace InvasionOfAldebaran.Helper
{
    public class RelayCommand : ICommand
    {
        private readonly Action _action;
        private readonly Action<string> _parameterAction;
		private readonly Action<object, EventArgs> _eventHandlerAction;

        public RelayCommand(Action action)
        {
            _action = action;
        }

        public RelayCommand(Action<string> parameterAction)
        {
            _parameterAction = parameterAction;
        }

		public RelayCommand(Action<object, EventArgs> eventHandlerAction)
		{
			this._eventHandlerAction = eventHandlerAction;
		}

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter == null)
                _action.Invoke();
            else
                _parameterAction.Invoke(parameter.ToString());
        }

        public event EventHandler CanExecuteChanged;
    }
}