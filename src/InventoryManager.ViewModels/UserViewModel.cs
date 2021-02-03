using InventoryManager.Commands;
using InventoryManager.Models;
using InventoryManager.Extensions;
using InventoryManager.Views;
using System.Collections.ObjectModel;

namespace InventoryManager.ViewModels
{
	public class UserViewModel : ViewModelBase
	{
		private readonly User _userModel;

		private ObservableCollection<User> _users;

		private string _inputtedLogin;

		private string _inputtedPassword;

		private string _inputtedFullName;

		public UserViewModel()
		{
			_userModel = new User();
			_users = _userModel.All().ToObservableCollection();

			AddUserCommand = new ButtonCommand(
				(obj) =>
				{
					var addUserView = new AddUserView();
					addUserView.ShowDialog();
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

			ShowAddUserViewCommand = new ButtonCommand(
				(o) =>
				{
					var addUserDialog = new AddUserView();
					addUserDialog.DataContext = this;
					addUserDialog.ShowDialog();
				}
			);
		}

		public User SelectedUser { get; set; }

		public ObservableCollection<User> Users => _users;

		public ButtonCommand AddUserCommand { get; }

		public ButtonCommand RemoveUserCommand { get; }

		public ButtonCommand ShowAddUserViewCommand { get; }

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

		public Group SelectedUserGroup { get; set; }
	}
}
