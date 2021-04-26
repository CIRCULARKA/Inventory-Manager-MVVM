using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Infrastructure;
using InventoryManager.Infrastructure.Filtering;
using System.Collections.Generic;
using Ninject;
using static InventoryManager.DependencyInjection.NinjectKernel;

namespace InventoryManager.ViewModels
{
	public class DevicesManagementViewModel : ViewModelBase
	{
		public DevicesManagementViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;
		}

		public IDeviceRelatedRepository Repository { get; }

		DevicesListView _devicesListPartialView = new DevicesListView();

		DeviceIPListView _deviceIPListPartialView = new DeviceIPListView();

		DeviceAccountsListView _deviceAccountsListPartialView = new DeviceAccountsListView();

		DeviceLocationView _deviceLocationPartialView = new DeviceLocationView();

		DeviceSearchAndFilteringView _deviceSearchAndFilteringPartialView = new DeviceSearchAndFilteringView();

		DeviceHistoryView _deviceHistoryPartialView = new DeviceHistoryView();

		SoftwareListView _softwareListPartialView = new SoftwareListView();

		public DevicesManagementViewModel()
		{
			var _devicesListViewModel = ResolveDependency<IDevicesListViewModel>();
			_devicesListPartialView.DataContext = _devicesListViewModel;

			var _deviceIPListViewModel = ResolveDependency<IDeviceIPListViewModel>();
			_deviceIPListPartialView.DataContext = _deviceIPListViewModel;

			var _deviceAccountsListViewModel = ResolveDependency<IDeviceAccountsListViewModel>();
			_deviceAccountsListPartialView.DataContext = _deviceAccountsListViewModel;

			var _deviceLocationViewModel = ResolveDependency<IDeviceLocationViewModel>();
			_deviceLocationPartialView.DataContext = _deviceLocationViewModel;

			var _deviceSearchAndFilteringViewModel = ResolveDependency<IDeviceSearchAndFilteringViewModel>();
			_deviceSearchAndFilteringPartialView.DataContext = _deviceSearchAndFilteringViewModel;

			var _deviceHistoryViewModel = ResolveDependency<IDeviceMovementHistoryViewModel>();
			_deviceHistoryPartialView.DataContext = _deviceHistoryViewModel;
		}

		public DevicesListView DevicesListPartialView => _devicesListPartialView;

		public DeviceIPListView DeviceIPListPartialView => _deviceIPListPartialView;

		public DeviceAccountsListView DeviceAccountsListPartialView => _deviceAccountsListPartialView;

		public DeviceLocationView DeviceLocationPartialView => _deviceLocationPartialView;

		public DeviceSearchAndFilteringView DeviceSearchAndFilteringPartialView =>
			_deviceSearchAndFilteringPartialView;

		public DeviceHistoryView DeviceHistoryPartialView => _deviceHistoryPartialView;

		public SoftwareListView SoftwareListPartialView => _softwareListPartialView;
	}
}
