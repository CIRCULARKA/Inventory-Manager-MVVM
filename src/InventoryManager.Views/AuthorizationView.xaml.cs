using InventoryManager.ViewModels;
using InventoryManager.Models;

namespace InventoryManager.Views
{
	public partial class AuthorizationView : ViewBase
	{
		public AuthorizationView()
		{
			InitializeComponent();

			ViewModel = new AuthorizationViewModel(this, new DefaultUserRelatedRepository());
			DataContext = ViewModel;
		}

		private AuthorizationViewModel ViewModel { get; }
	}
}
