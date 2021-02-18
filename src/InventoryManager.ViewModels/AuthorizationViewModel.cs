using InventoryManager.Commands;
using InventoryManager.Views;

namespace InventoryManager.ViewModels
{
	public class AuthorizationViewModel : ViewModelBase
	{
		public AuthorizationViewModel(ViewBase view)
		{
			AuthorizationView = view;

			LoginCommand = new ButtonCommand(
				(obj) =>
				{
					var findedUser = Users.Find(InputtedLogin);

					if (findedUser != null && findedUser.Password == InputtedPassword)
					{
						var mainView = new MainView();
						mainView.Show();
						AuthorizationView.Close();
					}
					else MessageToUser = "Логин или пароль введён неверно";
				}
			);
		}

		private ViewBase AuthorizationView { get; }

		public ButtonCommand LoginCommand { get; }

		public string InputtedLogin { get; set; }

		public string InputtedPassword { get; set; }
	}
}
