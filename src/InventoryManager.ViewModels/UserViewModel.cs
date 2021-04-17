using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Events;
using InventoryManager.Commands;
using InventoryManager.Extensions;
using InventoryManager.Infrastructure;
using System;
using System.Collections.ObjectModel;

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
				(obj) =>
				{
					if (UserSession.IsAuthorizedUserAllowedTo(UserActions.RemoveUser))
						return SelectedUser != null;
					else return false;
				}
			);

			ShowAddUserViewCommand = RegisterCommandAction(
				(obj) => AddUserView.ShowDialog(),
				(obj) =>
					UserSession.IsAuthorizedUserAllowedTo(UserActions.AddUser)
			);
		}

		private IUserRelatedRepository Repository { get; }

		public ObservableCollection<User> UsersToShow =>
			_allUsersToShow;

		public User SelectedUser { get; set; }

		public UserGroup SelectedUserGroup { get; set; }

		public Command RemoveUserCommand { get; }

		public Command ShowAddUserViewCommand { get; }

		public AddUserView AddUserView { get; set; }

		public AddUserViewModel AddUserViewModel =>
			ViewModelLinker.GetRegisteredViewModel<AddUserViewModel>();

		private void SubscribeActionOnUserAddition(Action<User> action) =>
			UserEvents.OnUserAdded += action;
	}
}
