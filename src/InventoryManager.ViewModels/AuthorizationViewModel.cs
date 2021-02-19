using InventoryManager.Commands;
using InventoryManager.Views;
using InventoryManager.Models;

namespace InventoryManager.ViewModels
{
	public class AuthorizationViewModel : ViewModelBase
	{
		public AuthorizationViewModel(ViewBase view, IUserRelatedRepository repo)
		{
			AuthorizationView = view;

			LoginCommand = new ButtonCommand(
				(obj) =>
				{
					var findedUser = Repository.FindUser(InputtedLogin);

					if (IsUserPasswordCorrect(findedUser))
					{
						var mainView = new MainView();
						mainView.Show();
						AuthorizationView.Close();
					}
					else MessageToUser = "Логин или пароль введён неверно";
				}
			);
		}

		private IUserRelatedRepository Repository { get; }

		private ViewBase AuthorizationView { get; }

		public ButtonCommand LoginCommand { get; }

		public string InputtedLogin { get; set; }

		public string InputtedPassword { get; set; }

		public bool IsUserPasswordCorrect(User user) =>
			user == null ? false : user.Password == InputtedPassword;
	}
}
