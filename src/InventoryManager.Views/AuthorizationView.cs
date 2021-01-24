using System.Windows;
using InventoryManager.ViewModels;

namespace InventoryManager
{
	public partial class AuthorizationView : Window
	{
		public AuthorizationView()
		{
			InitializeComponent();

			DataContext = new AuthorizationViewModel();
		}

		public void AttemptToLogin(object sender, RoutedEventArgs info)
		{
			// There is must be a call of a ViewModel method that confirms that user exists
			// If so, method must open user's window and close this view
		}
	}
}
