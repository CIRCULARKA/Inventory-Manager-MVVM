using InventoryManager.Models;

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
	}
}
