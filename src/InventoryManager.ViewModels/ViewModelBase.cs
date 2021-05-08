using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.DependencyInjection;
using System;

namespace InventoryManager.ViewModels
{
	public abstract class ViewModelBase : NotifyingModel
	{
		private string _messageToUser;

		private DependencyResolver _resolver;

		public ViewModelBase()
		{
			_resolver = new DependencyResolver();
		}

		private DependencyResolver Resolver => _resolver;

		public string MessageToUser
		{
			get => _messageToUser;
			set
			{
				_messageToUser = value;
				OnPropertyChanged("MessageToUser");
			}
		}

		public ViewBase RelatedView { get; set; }

		protected Command RegisterCommandAction(
			Action<object> action,
			Func<object, bool> conditionOfWork = null
		) => new Command(action, conditionOfWork);

		protected T ResolveDependency<T>() =>
			DependencyResolver.Resolve<T>();
	}
}
