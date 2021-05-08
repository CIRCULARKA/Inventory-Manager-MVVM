#define DEBUG

using InventoryManager.Views;
using InventoryManager.Events;
using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;
using System;
using System.Windows;

namespace InventoryManager.ViewModels
{
	public class AuthorizationViewModel : ViewModelBase, IUserSessionViewModel
	{
		private string _login;

		private string _password;

		private MainView _mainView;

		public AuthorizationViewModel(IUserRelatedRepository repo, IUserSession userSession)
		{
			Repository = repo;

			UserSession = userSession;

			LoginCommand = RegisterCommandAction(
				(obj) =>
				{
#if DEBUG
					AuthenticatedUser = Repository.FindUser("root");
					InputtedPassword = "root";
#else
					AuthenticatedUser = Repository.FindUser(InputtedLogin);
#endif

					if (IsInputtedPasswordCorrect())
					{
						RelatedView.Hide();

						UserEvents.RaiseOnUserLoggedIn(AuthenticatedUser);

						UserSession.AuthorizeUser(
							AuthenticatedUser,
							UserRightsBuilder.GetUserRights(
							UserSession.GetAccessLevel(AuthenticatedUser)
							)
						);

						_mainView = new MainView();
						_mainView.DataContext =
							ResolveDependency<IMainViewModel>()
								as MainViewModel;

						_mainView.Show();

					}
					else MessageToUser = "Логин или пароль введён неверно";
				}
			);

			UserEvents.OnUserLoggedOut += () =>
			{
				_mainView.Hide();

				InputtedLogin = InputtedPassword = string.Empty;

				(ResolveDependency<IAuthorizationView>() as Window).Show();
			};
		}
		private IUserRelatedRepository Repository { get; }

		public User AuthenticatedUser { get; set; }

		public IUserSession UserSession { get; }

		public Command LoginCommand { get; }

		public string InputtedLogin
		{
			get => _login;
			set
			{
				_login = value;
				OnPropertyChanged(nameof(InputtedLogin));
			}
		}

		public string InputtedPassword
		{
			get => _password;
			set
			{
				_password = value;
				OnPropertyChanged(nameof(InputtedPassword));
			}
		}

		public bool IsInputtedPasswordCorrect()
		{
			try
			{
				return AuthenticatedUser == null ?
					false :
					AuthenticatedUser.Password == InputtedPassword;
			}
			catch (NullReferenceException) { return false; }
			catch
			{
				throw new Exception(
					"Error has occured while checking " +
					"for correctness of inputted password"
				);
			}
		}
	}
}
