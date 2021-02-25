using System;
using System.Collections.ObjectModel;
using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Extensions;

namespace InventoryManager.ViewModels
{
	public class AddUserViewModel : ViewModelBase
	{
		private string _inputtedLogin;

		private string _inputtedPassword;

		private string _inputtedFirstName;

		private string _inputtedLastName;

		private string _inputtedMiddleName;

		public AddUserViewModel(IUserRelatedRepository repo)
		{
			Repository = repo;

			AddUserCommand = RegisterCommandAction(
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

						OnUserAdded?.Invoke(newUser);

						MessageToUser = "Пользователь добавлен";

						ClearInputFields();
					}
					catch (System.Exception)
					{
						MessageToUser = "Вы ввели не все данные, либо пользователь уже существует";
					}
				},
				(obj) => AreInputFieldsNotEmpty
			);
		}

		public event Action<User> OnUserAdded;

		private IUserRelatedRepository Repository { get; }

		public ObservableCollection<UserGroup> UserGroupsToShow =>
			Repository.AllUserGroups.ToObservableCollection();

		public UserGroup SelectedUserGroup { get; set; }

		public Command AddUserCommand { get; }

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

		public void ClearInputFields()
		{
			InputtedFirstName = string.Empty;
			InputtedLastName = string.Empty;
			InputtedMiddleName = string.Empty;
			InputtedLogin = string.Empty;
			InputtedPassword = string.Empty;
		}

		public bool AreInputFieldsNotEmpty =>
			!(
				string.IsNullOrWhiteSpace(InputtedFirstName) ||
				string.IsNullOrWhiteSpace(InputtedLastName) ||
				string.IsNullOrWhiteSpace(InputtedMiddleName) ||
				string.IsNullOrWhiteSpace(InputtedLogin) ||
				string.IsNullOrWhiteSpace(InputtedPassword) ||
				SelectedUserGroup == null
			);
	}
}
