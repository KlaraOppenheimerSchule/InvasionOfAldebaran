using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InvasionOfAldebaran.Models
{
	public class RelayCommand : ICommand
	{
		private Action _action;
		private Action<string> _parameterAction;
		
		public RelayCommand(Action action)
		{
			this._action = action;
		}

		public RelayCommand(Action<string> parameterAction)
		{
			this._parameterAction = parameterAction;
		}
		
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			if(parameter == null)
				this._action.Invoke();
			else
				this._parameterAction.Invoke(parameter.ToString());
		}

		public event EventHandler CanExecuteChanged;
	}
}
