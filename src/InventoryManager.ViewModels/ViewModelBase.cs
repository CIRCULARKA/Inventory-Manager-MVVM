using InventoryManager.Models;
using InventoryManager.Data;

namespace InventoryManager.ViewModels
{
	public class ViewModelBase : NotifyingModel
	{
		private string _messageToUser;

		protected ViewModelBase()
		{
			DataContext = new InventoryManagerDbContext();
		}

		public InventoryManagerDbContext DataContext { get; }

		public string MessageToUser
		{
			get => _messageToUser;
			set
			{
				_messageToUser = value;
				OnPropertyChanged("MessageToUser");
			}
		}
	}
}
