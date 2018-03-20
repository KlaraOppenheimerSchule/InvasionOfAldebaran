using System;
using System.Windows.Input;

namespace InvasionOfAldebaran.Helper
{
	public class RelayCommand : ICommand
	{
		private Action _action;
		private Action<string> _parameterAction;
		
		public RelayCommand(Action action)
		{
			_action = action;
		}

		public RelayCommand(Action<string> parameterAction)
		{
			_parameterAction = parameterAction;
		}
		
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			if(parameter == null)
				_action.Invoke();
			else
				_parameterAction.Invoke(parameter.ToString());
		}

		public event EventHandler CanExecuteChanged;
	}
}
