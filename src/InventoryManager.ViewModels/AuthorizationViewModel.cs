using InventoryManager.Views;
using InventoryManager.Events;
using InventoryManager.Models;
using InventoryManager.Commands;
using System;

namespace InventoryManager.ViewModels
{
	public class AuthorizationViewModel : ViewModelBase
	{
		private string _login;

		private string _password;

		private MainView _mainView;

		public AuthorizationViewModel(IUserRelatedRepository repo)
		{
			Repository = repo;

			LoginCommand = RegisterCommandAction(
				(obj) =>
				{
					AuthenticatedUser = Repository.FindUser(InputtedLogin);

					if (IsInputtedPasswordCorrect())
					{
						RelatedView.Hide();
						UserEvents.RaiseOnUserLoggedIn(AuthenticatedUser);

						var mainViewModel = new MainViewModel();

						_mainView = new MainView();
						_mainView.DataContext = mainViewModel;

						mainViewModel.RelatedView = _mainView;

						_mainView.Show();

					}
					else MessageToUser = "Логин или пароль введён неверно";
				}
			);

			UserEvents.OnUserLoggedOut += () =>
			{
				_mainView.Hide();

				InputtedLogin = InputtedPassword = string.Empty;

				RelatedView.Show();
			};
		}

		public User AuthenticatedUser { get; set; }

		private IUserRelatedRepository Repository { get; }

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
