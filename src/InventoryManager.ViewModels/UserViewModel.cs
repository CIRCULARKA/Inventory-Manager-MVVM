using InventoryManager.Commands;
using InventoryManager.Models;
using InventoryManager.Extensions;
using InventoryManager.Views;
using System.Collections.ObjectModel;
using System;

namespace InventoryManager.ViewModels
{
	public class UserViewModel : ViewModelBase
	{
		private ObservableCollection<User> _allUsersToShow;

		public UserViewModel(IUserRelatedRepository repo)
		{
			Repository = repo;
			AddUserViewModel = new AddUserViewModel(Repository);

			SubscribeActionOnUserAddition(
				(user) => UsersToShow.Add(user)
			);

			_allUsersToShow = Repository.AllUsers.ToObservableCollection();

			RemoveUserCommand = RegisterCommandAction(
				(obj) =>
				{
					var userToRemove = Repository.FindUser(SelectedUser.Login);
					Repository.RemoveUser(userToRemove);
					Repository.SaveChanges();
					UsersToShow.Remove(SelectedUser);
				},
				(obj) => SelectedUser != null
			);

			ShowAddUserViewCommand = RegisterCommandAction(
				(obj) =>
				{
					AddUserView = new AddUserView();
					AddUserView.DataContext = AddUserViewModel;
					AddUserView.ShowDialog();
				}
			);
		}

		private IUserRelatedRepository Repository { get; }

		public ObservableCollection<User> UsersToShow =>
			_allUsersToShow;

		public User SelectedUser { get; set; }

		public UserGroup SelectedUserGroup { get; set; }

		public ButtonCommand RemoveUserCommand { get; }

		public ButtonCommand ShowAddUserViewCommand { get; }

		public AddUserView AddUserView { get; private set; }

		public AddUserViewModel AddUserViewModel { get; set; }

		private void SubscribeActionOnUserAddition(Action<User> action)
		{
			if (AddUserViewModel != null)
				(AddUserViewModel as AddUserViewModel).OnUserAdded += action;
		}
	}
}
