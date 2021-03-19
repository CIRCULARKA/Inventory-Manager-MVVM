using InventoryManager.Commands;
using InventoryManager.Models;
using InventoryManager.Extensions;
using InventoryManager.Views;
using InventoryManager.Infrastructure;
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
				(obj) => SelectedUser != null && SelectedUser.UserGroup.Name != "Суперпользователь"
			);

			ShowAddUserViewCommand = RegisterCommandAction(
				(obj) => AddUserView.ShowDialog()
			);
		}

		private IUserRelatedRepository Repository { get; }

		public ObservableCollection<User> UsersToShow =>
			_allUsersToShow;

		public User SelectedUser { get; set; }

		public UserGroup SelectedUserGroup { get; set; }

		public Command RemoveUserCommand { get; }

		public Command ShowAddUserViewCommand { get; }

		public AddUserView AddUserView =>
			ViewModelLinker.GetRegisteredView<AddUserView>();

		public AddUserViewModel AddUserViewModel =>
			ViewModelLinker.GetRegisteredViewModel<AddUserViewModel>();

		private void SubscribeActionOnUserAddition(Action<User> action)
		{
			if (AddUserViewModel != null)
				(AddUserViewModel as AddUserViewModel).OnUserAdded += action;
		}
	}
}
