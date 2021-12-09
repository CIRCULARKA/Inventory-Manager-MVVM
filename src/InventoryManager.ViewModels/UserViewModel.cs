using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Events;
using InventoryManager.Commands;
using InventoryManager.Extensions;
using InventoryManager.UsersAccess;
using System;
using System.Collections.ObjectModel;

namespace InventoryManager.ViewModels
{
	public class UserViewModel : ViewModelBase, IUserSessionViewModel, IUserViewModel
	{
		private ObservableCollection<User> _allUsersToShow;

		public UserViewModel(IUserRelatedRepository repo, IUserSession userSession)
		{
			Repository = repo;

			UserSession = userSession;

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
					if (SelectedUser?.Login == "root") return false;
					if (UserSession.IsAuthorizedUserAllowedTo(UserActions.RemoveUser))
						return SelectedUser != null;
					else return false;
				}
			);

			ShowAddUserViewCommand = RegisterCommandAction(
				(obj) =>
				{
					var addUserView = new AddUserView();
					addUserView.DataContext = ResolveDependency<IAddUserViewModel>();
					addUserView.ShowDialog();
				},
				(obj) =>
					UserSession.IsAuthorizedUserAllowedTo(UserActions.AddUser)
			);
		}

		private IUserRelatedRepository Repository { get; }

		public IUserSession UserSession { get; }

		public ObservableCollection<User> UsersToShow =>
			_allUsersToShow;

		public User SelectedUser { get; set; }

		public Command RemoveUserCommand { get; }

		public Command ShowAddUserViewCommand { get; }

		private void SubscribeActionOnUserAddition(Action<User> action) =>
			UserEvents.OnUserAdded += action;
	}
}
