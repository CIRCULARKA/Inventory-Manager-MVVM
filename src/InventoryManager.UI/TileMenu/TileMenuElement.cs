using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace InventoryManager.UI
{
	public class TileMenuElement : UserControl
	{
		public Window ActiveWindow =>
			Application.
				Current.
					Windows.
						OfType<Window>().
							First(w => w.IsActive) as Window;

		protected void DragParentWindow(object sender, MouseButtonEventArgs info)
		{
			if (info.Source is Grid || info.Source is TextBlock)
				ActiveWindow.DragMove();
		}
	}
}
