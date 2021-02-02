using InventoryManager.Commands;
using InventoryManager.Models;
using InventoryManager.Extensions;
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
			_users = _userModel.All().ToObservableCollection();

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

		public ObservableCollection<User> Users => _users;

		public ButtonCommand AddUserCommand { get; }

		public ButtonCommand RemoveUserCommand { get; }
	}
}
