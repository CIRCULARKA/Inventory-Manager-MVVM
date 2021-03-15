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

			var repo = new DefaultDeviceRelatedRepository();
			DataContext = new DeviceViewModel(
				repo, new AddDeviceViewModel(repo), new DeviceIPViewModel(repo),
				new DeviceAccountViewModel(repo)
			);
		}
	}
}
