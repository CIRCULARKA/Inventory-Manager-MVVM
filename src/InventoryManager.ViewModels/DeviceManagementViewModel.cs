using InventoryManager.Views;
using InventoryManager.Infrastructure;

namespace InventoryManager.ViewModels
{
	public class DevicesManagementViewModel : ViewModelBase
	{
		public DevicesListView DevicesListPartialView =>
			ViewModelLinker.GetRegisteredPartialView<DevicesListView>();

		public DeviceIPListView DeviceIPListPartialView =>
			ViewModelLinker.GetRegisteredPartialView<DeviceIPListView>();

		public DeviceAccountsListView DeviceAccountsListPartialView =>
			ViewModelLinker.GetRegisteredPartialView<DeviceAccountsListView>();

		public DeviceLocationView DeviceLocationPartialView =>
			ViewModelLinker.GetRegisteredPartialView<DeviceLocationView>();

		public DeviceSearchAndFilteringView DeviceSearchAndFilteringPartialView =>
			ViewModelLinker.GetRegisteredPartialView<DeviceSearchAndFilteringView>();

		public DeviceHistoryView DeviceHistoryPartialView =>
			ViewModelLinker.GetRegisteredPartialView<DeviceHistoryView>();
	}
}
