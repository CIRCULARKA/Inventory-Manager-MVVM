using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InventoryManager.Views
{
	public class PartialViewBase : UserControl
	{
		public Window ActiveWindow() =>
			Application.
				Current.
					Windows.
						OfType<Window>().
							First(w => w.IsActive) as Window;

	}
}
