using InventoryManager.Commands;
using InventoryManager.Models;
using System.Collections.ObjectModel;

namespace InventoryManager.ViewModels
{
	public class UserViewModel : ViewModelBase
	{
		private readonly User _userModel;

		private ObservableCollection<User> _users;

		public UserViewModel()
		{
			_userModel = new User();

			// Need to make something like All() method for models
			// _users =

			AddUserCommand = new ButtonCommand(
				(obj) =>
				{
				}
			);

			RemoveUserCommand = new ButtonCommand(
				(obj) =>
				{
					_userModel.Remove(SelectedUser);
					_userModel.SaveChanges();
				},
				(obj) => SelectedUser != null
			);
		}

		public User SelectedUser { get; set; }

		public ButtonCommand AddUserCommand { get; }

		public ButtonCommand RemoveUserCommand { get; }
	}
}
