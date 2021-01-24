using System.Windows;
using Microsoft.EntityFrameworkCore;
using InventoryManager.Models;
using InventoryManager.Data;

namespace InventoryManager.ViewModels
{
	public class AuthorizationViewModel : NotifyingModel
	{
		private string _inputtedLogin;

		private string _inputtedPassword;

		private string _messageToUser;

		public AuthorizationViewModel(InventoryManagerDbContext data)
		{
			Data = data;
		}

		private InventoryManagerDbContext Data { get; }

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

		public bool DoesUserExist()
		{
			return Data.User.Find(InputtedLogin) == null ? false : true;
		}
	}
}
