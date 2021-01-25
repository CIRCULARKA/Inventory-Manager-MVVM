using System.Windows;
using Microsoft.EntityFrameworkCore;
using InventoryManager.Models;
using InventoryManager.Data;

namespace InventoryManager.ViewModels
{
	public class AuthorizationViewModel : NotifyingModel
	{
		private string _messageToUser;

		public AuthorizationViewModel(InventoryManagerDbContext data)
		{
			Data = data;
		}

		private InventoryManagerDbContext Data { get; }

		public string InputtedLogin { get; set; }


		public string InputtedPassword { get; set; }

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
			var foundedUser = Data.User.Find(InputtedLogin);
			if (foundedUser != null & foundedUser.Password == InputtedPassword)
				return true;
			return false;
		}
	}
}
