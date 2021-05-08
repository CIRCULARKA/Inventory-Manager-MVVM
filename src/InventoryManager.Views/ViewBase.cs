using System.Windows;
using System.ComponentModel;

namespace InventoryManager.Views
{
	public class ViewBase : Window
	{
		public ViewBase() { }

		protected override void OnClosing(CancelEventArgs info) =>
			Application.Current.Shutdown();
	}
}
