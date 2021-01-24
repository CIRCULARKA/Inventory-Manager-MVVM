using System.Windows;
using InventoryManager.Data;
using InventoryManager.ViewModels;

namespace InventoryManager.Views
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
			if (ViewModel.DoesUserExist())
			{
				var userView = new UserView();
				userView.Show();
				this.Close();
			}
			else
			{
				ViewModel.MessageToUser = "Логин или пароль введён неверно";
			}
		}
	}
}
