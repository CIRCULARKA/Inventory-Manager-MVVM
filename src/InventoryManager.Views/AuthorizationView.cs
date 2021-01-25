using System.Windows;
using InventoryManager.ViewModels;

namespace InventoryManager.Views
{
	public partial class AuthorizationView : ViewBase
	{
		public AuthorizationView()
		{
			InitializeComponent();

			ViewModel = new AuthorizationViewModel(Data, this);
			DataContext = ViewModel;
		}

		private AuthorizationViewModel ViewModel { get; }
	}
}
