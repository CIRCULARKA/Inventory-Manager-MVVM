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
	}
}
