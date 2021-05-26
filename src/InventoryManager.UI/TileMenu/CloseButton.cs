using InventoryManager.Views;
using System.Windows;
using System.Windows.Input;

namespace InventoryManager.UI
{
	public partial class CloseButton : TileMenuElement
	{
		public CloseButton() =>
			InitializeComponent();

		public void CloseApplication(object sender, MouseButtonEventArgs info)
		{
			var activeWindow = base.ActiveWindow;

			if (activeWindow is AuthorizationView ||
				activeWindow is MainView)
				Application.Current.Shutdown();
			else
				activeWindow.Close();
		}

	}
}