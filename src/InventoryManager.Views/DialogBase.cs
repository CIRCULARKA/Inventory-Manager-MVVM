using System.ComponentModel;

namespace InventoryManager.Views
{
	public class DialogBase : ViewBase
	{
		public DialogBase() { }

		protected override void OnClosing(CancelEventArgs info)
		{
			info.Cancel = true;
			this.Hide();
		}
	}
}
