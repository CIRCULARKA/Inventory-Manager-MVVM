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

		private string _inputtedFirstName;

		private string _inputtedLastName;

		private string _inputtedMiddleName;

		public UserViewModel()
		{
			_userModel = new User();
			_users = _userModel.All().ToObservableCollection();

			AddUserCommand = new ButtonCommand(
				(obj) =>
				{
					var newUser = new User
					{
						LastName = InputtedLastName,
						FirstName = InputtedFirstName,
						MiddleName = InputtedMiddleName,
						Login = InputtedLogin,
						Password = InputtedPassword,
						UserGroup = SelectedUserGroup
					};
					_userModel.Add(newUser);
					Users.Add(newUser);
					_userModel.SaveChanges();
				},
				(obj) => !(string.IsNullOrWhiteSpace(InputtedLogin) &&
					string.IsNullOrWhiteSpace(InputtedPassword) &&
					string.IsNullOrWhiteSpace(InputtedFirstName) &&
					string.IsNullOrWhiteSpace(InputtedLastName) &&
					string.IsNullOrWhiteSpace(InputtedMiddleName) &&
					string.IsNullOrWhiteSpace(InputtedPassword))
			);

			RemoveUserCommand = new ButtonCommand(
				(obj) =>
				{
					_userModel.Remove(SelectedUser);
					_userModel.SaveChanges();
					Users.Remove(SelectedUser);
				},
				(obj) => SelectedUser != null
			);

			ShowAddUserViewCommand = new ButtonCommand(
				(obj) =>
				{
					var addUserDialog = new AddUserView();
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

		public string InputtedFirstName
		{
			get => _inputtedFirstName;
			set
			{
				_inputtedFirstName = value;
				OnPropertyChanged("InputtedFirstName");
			}
		}

		public string InputtedLastName
		{
			get => _inputtedLastName;
			set
			{
				_inputtedLastName = value;
				OnPropertyChanged("InputtedLastName");
			}
		}

		public string InputtedMiddleName
		{
			get => _inputtedMiddleName;
			set
			{
				_inputtedMiddleName = value;
				OnPropertyChanged("InputtedMiddleName");
			}
		}

		public Group SelectedUserGroup { get; set; }
	}
}
