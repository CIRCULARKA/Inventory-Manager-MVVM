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

		private readonly Group _groupModel;

		private ObservableCollection<User> _users;

		private ObservableCollection<Group> _groups;

		private string _inputtedLogin;

		private string _inputtedPassword;

		private string _inputtedFirstName;

		private string _inputtedLastName;

		private string _inputtedMiddleName;

		public UserViewModel()
		{
			_userModel = new User();
			_users = _userModel.All().ToObservableCollection();

			_groupModel = new Group();
			_groups = _groupModel.All().ToObservableCollection();

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
						UserGroupID = SelectedUserGroup.ID
					};

					try
					{
						_userModel.Add(newUser);
						_userModel.SaveChanges();
						UsersToShow.Add(newUser);

						MessageToUser = "Пользователь добавлен";

						InputtedFirstName = "";
						InputtedLastName = "";
						InputtedMiddleName = "";
						InputtedLogin = "";
						InputtedPassword = "";
					}
					catch (System.Exception)
					{
						MessageToUser = "Вы не ввели все данные, либо пользователь уже существует";
					}
				},
				(obj) => !(string.IsNullOrWhiteSpace(InputtedLogin) ||
					string.IsNullOrWhiteSpace(InputtedPassword) ||
					string.IsNullOrWhiteSpace(InputtedFirstName) ||
					string.IsNullOrWhiteSpace(InputtedLastName) ||
					string.IsNullOrWhiteSpace(InputtedMiddleName) ||
					string.IsNullOrWhiteSpace(InputtedPassword)) && SelectedUserGroup != null
			);

			RemoveUserCommand = new ButtonCommand(
				(obj) =>
				{
					_userModel.Remove(_userModel.Find(SelectedUser.Login));
					_userModel.SaveChanges();
					UsersToShow.Remove(SelectedUser);
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

		public ObservableCollection<User> UsersToShow => _users;

		public ObservableCollection<Group> UserGroups => _groups;

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
