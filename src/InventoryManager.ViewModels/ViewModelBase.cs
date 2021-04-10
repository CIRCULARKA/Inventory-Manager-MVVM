using InventoryManager.Views;
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

		/// <summary>
		/// View that linked with current ViewModel
		/// </summary>
		public ViewBase CorrespondingView { get; set; }

		protected Command RegisterCommandAction(
			Action<object> action,
			Func<object, bool> conditionOfWork = null
		) => new Command(action, conditionOfWork);

		protected void ShowView(ViewBase view, ViewModelBase dataContext)
		{
			view.DataContext = dataContext;
			view.Show();
		}
	}
}
