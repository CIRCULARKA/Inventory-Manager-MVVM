using InventoryManager.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace InventoryManager.UI
{
	public partial class CloseButton : UserControl
	{
		public CloseButton() =>
			InitializeComponent();

		public void CloseApplication(object sender, MouseButtonEventArgs info)
		{
			var activeWindow = Application.
				Current.
					Windows.
						OfType<Window>().
							First(w => w.IsActive) as Window;

			if (activeWindow is AuthorizationView ||
				activeWindow is MainView)
				Application.Current.Shutdown();
			else
				activeWindow.Close();
		}
	}
}
