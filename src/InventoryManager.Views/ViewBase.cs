using System.Windows;
using System.ComponentModel;
using InventoryManager.Data;

namespace InventoryManager.Views
{
	public class ViewBase : Window
	{
		public ViewBase() { }

		protected override void OnClosing(CancelEventArgs info)
		{
			info.Cancel = true;
			this.Hide();
		}
	}
}
