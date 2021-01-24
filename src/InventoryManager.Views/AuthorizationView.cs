using System.Windows;
using InventoryManager.Data;
using InventoryManager.ViewModels;

namespace InventoryManager
{
	public partial class AuthorizationView : Window
	{
		public AuthorizationView()
		{
			InitializeComponent();

			ViewModel = new AuthorizationViewModel(new InventoryManagerDbContext());
			DataContext = ViewModel;
		}

		private AuthorizationViewModel ViewModel { get; }

		public void AttemptToLogin(object sender, RoutedEventArgs info)
		{
			// There is must be a call of a ViewModel method that confirms that user exists
			// If so, method must open user's window and close this view
			if (ViewModel.DoesUserExist())
			{
				// Message boxes as a temporary solution
				MessageBox.Show(
					"Вы успешно авторизированы",
					"Добро пожаловать!",
					MessageBoxButton.OK,
					MessageBoxImage.Information
				);
			}
			else
			{
				ViewModel.MessageToUser = "Логин или пароль введён неверно";
			}
		}
	}
}
