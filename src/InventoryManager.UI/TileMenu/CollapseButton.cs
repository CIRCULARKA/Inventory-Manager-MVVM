using InventoryManager.Views;
using InventoryManager.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InventoryManager.UI
{
	public partial class CollapseButton : UserControl
	{
		public CollapseButton() =>
			InitializeComponent();

		public void CollapseWindow(object sender, MouseButtonEventArgs info) =>
			(DependencyResolver.Resolve<IAuthorizationView>() as Window).
				WindowState = WindowState.Minimized;
	}
}
