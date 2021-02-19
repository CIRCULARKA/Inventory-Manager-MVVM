using System.Windows.Controls;
using InventoryManager.ViewModels;
using InventoryManager.Models;

namespace InventoryManager.Views
{
	public partial class DevicesManagementView : UserControl
	{
		public DevicesManagementView()
		{
			InitializeComponent();
			DataContext = new DeviceViewModel(new DefaultDeviceRelatedRepository());
		}
	}
}
