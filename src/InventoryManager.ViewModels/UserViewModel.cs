using InventoryManager.Commands;
using InventoryManager.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace InventoryManager.ViewModels
{
	public class UserViewModel : ViewModelBase
	{
		private readonly User _userModel;

		public UserViewModel()
		{
			_userModel = new User();

			// AddUserCommand = new ButtonCommand(
			// 	(obj) =>
			// 	{

			// 	}
			// );
		}

		public ButtonCommand AddUserCommand { get; }

		public ButtonCommand RemoveUserCommand { get; }
	}
}
