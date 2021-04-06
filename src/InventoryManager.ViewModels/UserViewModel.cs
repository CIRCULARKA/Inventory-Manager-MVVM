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
				(obj) => SelectedUser != null && SelectedUser.UserGroup.Name != "Суперпользователь" // &&
					// base.AuthorizedUser.IsAllowedTo(UserActions.RemoveUser)
			);

			ShowAddUserViewCommand = RegisterCommandAction(
				(obj) => AddUserView.ShowDialog(),
				(obj) =>
				{
					// if (AuthorizedUser != null)
					// 	return base.AuthorizedUser.IsAllowedTo(UserActions.AddUser);
					// else return false;
					return true;
				}
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

		private void SubscribeActionOnUserAddition(Action<User> action) =>
			UserEvents.OnUserAdded += action;
	}
}
