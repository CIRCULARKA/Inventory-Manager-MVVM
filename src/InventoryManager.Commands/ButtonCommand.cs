using System;
using System.Windows.Input;

namespace InventoryManager.Commands
{
	public class ButtonCommand : ICommand
	{
		private Action<object> _execute;

		private Func<object, bool> _canExecute;

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public ButtonCommand(Action<object> action, Func<object, bool> canExecute = null)
		{
			_execute = action;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
			=> _canExecute == null || _canExecute(parameter);

		public void Execute(object parameter) =>
			_execute(parameter);
	}
}
