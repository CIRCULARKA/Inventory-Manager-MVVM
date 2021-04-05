using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;
using System;

namespace InventoryManager.ViewModels
{
	public abstract class ViewModelBase : NotifyingModel
	{
		private string _messageToUser;

		public string MessageToUser
		{
			get => _messageToUser;
			set
			{
				_messageToUser = value;
				OnPropertyChanged("MessageToUser");
			}
		}

		public AuthorizedUser AuthorizedUser { get; set; }

		protected Command RegisterCommandAction(
			Action<object> action,
			Func<object, bool> conditionOfWork = null
		) => new Command(action, conditionOfWork);
	}
}
