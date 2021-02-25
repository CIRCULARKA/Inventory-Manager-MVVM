using InventoryManager.Commands;
using InventoryManager.Views;
using InventoryManager.Models;

namespace InventoryManager.ViewModels
{
	public class AuthorizationViewModel : ViewModelBase
	{
		public AuthorizationViewModel(ViewBase view, IUserRelatedRepository repo)
		{
			Repository = repo;
			AuthorizationView = view;

			LoginCommand = RegisterCommandAction(
				(obj) =>
				{
					AuthorizingUser = Repository.FindUser(InputtedLogin);

					if (IsUserPasswordCorrect())
					{
						var mainView = new MainView();
						mainView.Show();
						AuthorizationView.Close();
					}
					else MessageToUser = "Логин или пароль введён неверно";
				}
			);
		}

		public User AuthorizingUser { get; set; }

		private IUserRelatedRepository Repository { get; }

		private ViewBase AuthorizationView { get; }

		public Command LoginCommand { get; }

		public string InputtedLogin { get; set; }

		public string InputtedPassword { get; set; }

		public bool IsUserPasswordCorrect() =>
			AuthorizingUser == null ? false : AuthorizingUser.Password == InputtedPassword;
	}
}
