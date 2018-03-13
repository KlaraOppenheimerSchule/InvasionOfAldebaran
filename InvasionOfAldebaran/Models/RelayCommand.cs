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
		
		public RelayCommand(Action action)
		{
			this._action = action;
		}
		
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			this._action.Invoke();
		}

		public event EventHandler CanExecuteChanged;
	}
}
