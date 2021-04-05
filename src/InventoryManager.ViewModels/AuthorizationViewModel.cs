using InventoryManager.Commands;
using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Infrastructure;

namespace InventoryManager.ViewModels
{
	public class AuthorizationViewModel : ViewModelBase
	{
		public AuthorizationViewModel(IUserRelatedRepository repo)
		{
			Repository = repo;

			LoginCommand = RegisterCommandAction(
				(obj) =>
				{
					AuthorizingUser = Repository.FindUser(InputtedLogin);

					if (IsUserPasswordCorrect())
					{
						AuthorizationView.Hide();

						MainViewModel.AuthorizedUser = AuthorizingUser;
						MainViewModel.LoadTabItemsContent();

						MainView.Show();
					}
					else MessageToUser = "Логин или пароль введён неверно";
				}
			);
		}

		public User AuthorizingUser { get; set; }

		private IUserRelatedRepository Repository { get; }

		private ViewBase AuthorizationView =>
			ViewModelLinker.GetRegisteredView<AuthorizationView>();

		public MainViewModel MainViewModel =>
			ViewModelLinker.GetRegisteredViewModel<MainViewModel>();

		public MainView MainView =>
			ViewModelLinker.GetRegisteredView<MainView>();

		public Command LoginCommand { get; }

		public string InputtedLogin { get; set; }

		public string InputtedPassword { get; set; }

		public bool IsUserPasswordCorrect() =>
			AuthorizingUser == null ? false : AuthorizingUser.Password == InputtedPassword;
	}
}
