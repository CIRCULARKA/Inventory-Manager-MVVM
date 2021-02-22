using InventoryManager.Commands;
using InventoryManager.Models;
using InventoryManager.Extensions;
using InventoryManager.Views;
using System.Collections.ObjectModel;

namespace InventoryManager.ViewModels
{
	public class UserViewModel : ViewModelBase
	{
		private ObservableCollection<User> _allUsersToShow;

		public UserViewModel(IUserRelatedRepository repo, ViewModelBase addUserVM, ViewBase addUserView)
		{
			Repository = repo;

			AddUserViewModel = addUserVM;
			(AddUserViewModel as AddUserViewModel).OnUserAdded +=
				(user) => UsersToShow.Add(user);

			AddUserView = addUserView;

			_allUsersToShow = Repository.AllUsers.ToObservableCollection();

			RemoveUserCommand = new ButtonCommand(
				(obj) =>
				{
					var userToRemove = Repository.FindUser(SelectedUser.Login);
					Repository.RemoveUser(userToRemove);
					Repository.SaveChanges();
					UsersToShow.Remove(SelectedUser);
				},
				(obj) => SelectedUser != null
			);

			ShowAddUserViewCommand = new ButtonCommand(
				(obj) =>
				{
					var addUserDialog = new AddUserView();
					addUserDialog.DataContext = AddUserViewModel;
					addUserDialog.ShowDialog();
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

		public ViewBase AddUserView { get; }

		public ViewModelBase AddUserViewModel { get; }
	}
}
