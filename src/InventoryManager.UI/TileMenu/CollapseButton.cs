using System.Windows;
using System.Windows.Input;

namespace InventoryManager.UI
{
	public partial class CollapseButton : TileMenuElement
	{
		public CollapseButton() =>
			InitializeComponent();

		public void CollapseWindow(object sender, MouseButtonEventArgs info) =>
			ActiveWindow.WindowState = WindowState.Minimized;
	}
}
