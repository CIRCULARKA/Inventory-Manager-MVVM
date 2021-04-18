using InventoryManager.Views;
using InventoryManager.Events;

namespace InventoryManager.ViewModels
{
	public class DevicesManagementViewModel : ViewModelBase
	{
		public DevicesManagementViewModel()
		{
			DevicesListPartialView = new DevicesListView();
			DeviceIPListPartialView = new DeviceIPListView();
			DeviceAccountsListPartialView = new DeviceAccountsListView();
			DeviceLocationPartialView = new DeviceLocationView();
			DeviceSearchAndFilteringPartialView = new DeviceSearchAndFilteringView();
			DeviceHistoryPartialView = new DeviceHistoryView();
		}

		public DevicesListView DevicesListPartialView { get; }

		public DeviceIPListView DeviceIPListPartialView { get; }

		public DeviceAccountsListView DeviceAccountsListPartialView { get; }

		public DeviceLocationView DeviceLocationPartialView { get; }

		public DeviceSearchAndFilteringView DeviceSearchAndFilteringPartialView { get; }

		public DeviceHistoryView DeviceHistoryPartialView { get; }
	}
}
