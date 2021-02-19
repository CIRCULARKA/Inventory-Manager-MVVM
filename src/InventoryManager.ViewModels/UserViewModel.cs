using InventoryManager.Commands;
using InventoryManager.Models;
using InventoryManager.Extensions;
using InventoryManager.Views;
using System.Collections.ObjectModel;

namespace InventoryManager.ViewModels
{
	public class UserViewModel : ViewBase
	{
		private string _inputtedLogin;

		private string _inputtedPassword;

		private string _inputtedFirstName;

		private string _inputtedLastName;

		private string _inputtedMiddleName;

		public UserViewModel(IUserRelatedRepository repo)
		{
			Repository = repo;

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
						Repository.AddUser(newUser);
						Repository.SaveChanges();

						// Load user group explicitly to display user group in users list
						newUser.UserGroup = SelectedUserGroup;
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
					var userToRemove = Users.Find(SelectedUser.Login);
					Users.Remove(userToRemove);
					Users.SaveChanges();
					UsersToShow.Remove(SelectedUser);
				},
				(obj) => SelectedUser != null
			);

			ShowAddUserViewCommand = new ButtonCommand(
				(obj) =>
				{
					var addUserDialog = new AddUserView();
					addUserDialog.DataContext = this;
					addUserDialog.ShowDialog();
				}
			);
		}

		private IUserRelatedRepository Repository { get; }

		public ObservableCollection<User> UsersToShow =>
			Users.All.ToObservableCollection();

		public ObservableCollection<UserGroup> UserGroupsToShow =>
			Users.AllUserGroups.ToObservableCollection();

		public User SelectedUser { get; set; }

		public UserGroup SelectedUserGroup { get; set; }

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
	}
}
