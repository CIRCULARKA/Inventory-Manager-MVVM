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

			LoginCommand = new ButtonCommand(
				(obj) =>
				{
					AuthorizedUser = Repository.FindUser(InputtedLogin);

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

		public User AuthorizedUser { get; set; }

		private IUserRelatedRepository Repository { get; }

		private ViewBase AuthorizationView { get; }

		public ButtonCommand LoginCommand { get; }

		public string InputtedLogin { get; set; }

		public string InputtedPassword { get; set; }

		public bool IsUserPasswordCorrect() =>
			AuthorizedUser == null ? false : AuthorizedUser.Password == InputtedPassword;
	}
}
