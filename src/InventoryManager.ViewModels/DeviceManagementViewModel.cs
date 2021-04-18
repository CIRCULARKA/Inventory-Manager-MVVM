using InventoryManager.Views;
using InventoryManager.Models;
using Ninject;
using static InventoryManager.DependencyInjection.NinjectKernel;

namespace InventoryManager.ViewModels
{
	public class DevicesManagementViewModel : ViewModelBase
	{
		public DevicesManagementViewModel()
		{
			var _devicesListViewModel = new DevicesListViewModel(
				StandartNinjectKernel.Get<IDeviceRelatedRepository>()
			);
			var _devicesListPartialView = new DevicesListView();
			_devicesListPartialView.DataContext = _devicesListViewModel;

			var _deviceIPListViewModel = new DeviceIPListViewModel(
				StandartNinjectKernel.Get<IDeviceRelatedRepository>()
			);
			var _deviceIPListPartialView = new DeviceIPListView();
				_deviceIPListPartialView.DataContext = _deviceIPListPartialView;

			var _deviceAccountsListViewModel = new DeviceAccountsListViewModel(
				StandartNinjectKernel.Get<IDeviceRelatedRepository>()
			);
			var _deviceAccountsListPartialView = new DeviceAccountsListView();
			_deviceAccountsListPartialView.DataContext = _deviceAccountsListViewModel;

			var _deviceLocationViewModel = new DeviceLocationViewModel(
				StandartNinjectKernel.Get<IDeviceRelatedRepository>()
			);
			var _deviceLocationPartialView = new DeviceLocationView();
			_deviceLocationPartialView.DataContext = _deviceLocationViewModel;

			var _deviceSearchAndFilteringViewModel = new DeviceSearchAndFilteringViewModel(
				new DeviceFilter()
			);
			var _deviceSearchAndFilteringPartialView = new DeviceSearchAndFilteringView();
			_deviceSearchAndFilteringPartialView.DataContext = _deviceSearchAndFilteringViewModel;

			var _deviceHistoryViewModel = new DeviceHistoryViewModel(
				StandartNinjectKernel.Get<IDeviceRelatedRepository>()
			);
			var _deviceHistoryPartialView = new DeviceHistoryView();
			_deviceHistoryPartialView.DataContext = _deviceHistoryViewModel;
		}
	}
}
