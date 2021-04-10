using InventoryManager.Views;
using InventoryManager.Events;
using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;

namespace InventoryManager.ViewModels
{
	public class AuthorizationViewModel : ViewModelBase
	{
		public AuthorizationViewModel(IUserRelatedRepository repo)
		{
			ViewModelEvents.RaiseOnViewModelInitiated(this);

			Repository = repo;

			LoginCommand = RegisterCommandAction(
				(obj) =>
				{
					AuthorizingUser = Repository.FindUser(InputtedLogin);

					if (IsUserPasswordCorrect())
					{
						RelatedView.Hide();
						UserEvents.RaiseOnUserLoggedIn(AuthorizingUser);

						ShowView(
							new MainView(),
							new MainViewModel()
						);
					}
					else MessageToUser = "Логин или пароль введён неверно";
				}
			);

			UserEvents.OnUserLoggedOut += Logout;
		}

		public User AuthorizingUser { get; set; }

		private IUserRelatedRepository Repository { get; }

		public Command LoginCommand { get; }

		public string InputtedLogin { get; set; }

		public string InputtedPassword { get; set; }

		public bool IsUserPasswordCorrect() =>
			AuthorizingUser == null ? false : AuthorizingUser.Password == InputtedPassword;

		private void ClearLoginAndPassword() =>
			InputtedLogin = InputtedPassword = string.Empty;

		private void Logout()
		{
			ViewModelLinker.
				GetRegisteredViewModel<MainViewModel>().
					RelatedView.
						Hide();

			ClearLoginAndPassword();
			RelatedView.Show();
		}
	}
}
