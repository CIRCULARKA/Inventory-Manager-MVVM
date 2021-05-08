using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace InventoryManager.UI
{
	public partial class CloseButton : UserControl
	{
		public CloseButton() =>
			InitializeComponent();

		public void CloseApplication(object sender, MouseButtonEventArgs info) =>
			Application.Current.Shutdown();
	}
}
