using InventoryManager.Commands;
using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Data;

namespace InventoryManager.ViewModels
{
	public class AuthorizationViewModel : ViewModelBase
	{
		private readonly User _userModel;

		public AuthorizationViewModel(ViewBase view)
		{
			_userModel = new User();

			AuthorizationView = view;
			LoginCommand = new ButtonCommand(
				(user) =>
				{
					try
					{
						var findedUser = _userModel.Find(InputtedLogin);

						var mainView = new MainView();
						mainView.Show();
						AuthorizationView.Close();
					}
					catch (System.Exception)
					{
						MessageToUser = "Логин или пароль введён неверно";
					}
				}
			);
		}

		private ViewBase AuthorizationView { get; }

		public ButtonCommand LoginCommand { get; }

		public string InputtedLogin { get; set; }

		public string InputtedPassword { get; set; }
	}
}
