using System.Windows;
using InventoryManager.Models;

namespace InventoryManager.ViewModels
{
	public class AuthorizationViewModel : NotifyingModel
	{
		private string _inputtedLogin;

		private string _inputtedPassword;

		private string _messageToUser;

		public string InputtedLogin
		{
			get => _inputtedLogin;
			set
			{
				_inputtedLogin = value;
				OnPropertyChanged("InputtedLogin");
			}
		}

		public string InputtedPassword
		{
			get => _inputtedPassword;
			set
			{
				_inputtedPassword = value;
				OnPropertyChanged("InputtedPassword");
			}
		}

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
