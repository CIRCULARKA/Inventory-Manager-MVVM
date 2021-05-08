using System.Windows.Controls;
using System.Windows.Input;

namespace InventoryManager.UI
{
	public partial class CollapseButton : UserControl
	{
		public CollapseButton() =>
			InitilizeComponent();

		public void CollapseWindow(object sender, MouseButtonEventArgs info) =>
			this.CollapseWindow(sender, info);
	}
}
