using InventoryManager.Views;
using InventoryManager.Models;
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

		public DevicesManagementViewModel()
		{
			var _devicesListViewModel = new DevicesListViewModel(
				StandartNinjectKernel.Get<IDeviceRelatedRepository>()
			);
			_devicesListPartialView.DataContext = _devicesListViewModel;

			var _deviceIPListViewModel = new DeviceIPListViewModel(
				StandartNinjectKernel.Get<IDeviceRelatedRepository>()
			);
				_deviceIPListPartialView.DataContext = _deviceIPListPartialView;

			var _deviceAccountsListViewModel = new DeviceAccountsListViewModel(
				StandartNinjectKernel.Get<IDeviceRelatedRepository>()
			);
			_deviceAccountsListPartialView.DataContext = _deviceAccountsListViewModel;

			var _deviceLocationViewModel = new DeviceLocationViewModel(
				StandartNinjectKernel.Get<IDeviceRelatedRepository>()
			);
			_deviceLocationPartialView.DataContext = _deviceLocationViewModel;

			var _deviceSearchAndFilteringViewModel = new DeviceSearchAndFilteringViewModel(
				new DeviceFilter(
					new List<DeviceFilteringCriteria>()
					{
						new DeviceFilteringCriteria("Сервер"),
						new	DeviceFilteringCriteria("Персональный компьютер"),
						new DeviceFilteringCriteria("Коммутатор")
					}
				)
			);
			_deviceSearchAndFilteringPartialView.DataContext = _deviceSearchAndFilteringViewModel;

			var _deviceHistoryViewModel = new DeviceHistoryViewModel(
				StandartNinjectKernel.Get<IDeviceRelatedRepository>()
			);
			_deviceHistoryPartialView.DataContext = _deviceHistoryViewModel;
		}

		public DevicesListView DevicesListPartialView => _devicesListPartialView;

		public DeviceIPListView DeviceIPListPartialView => _deviceIPListPartialView;

		public DeviceAccountsListView DeviceAccountsListPartialView => _deviceAccountsListPartialView;

		public DeviceLocationView DeviceLocationPartialView => _deviceLocationPartialView;

		public DeviceSearchAndFilteringView DeviceSearchAndFilteringPartialView =>
			_deviceSearchAndFilteringPartialView;

		public DeviceHistoryView DeviceHistoryPartialView => _deviceHistoryPartialView;
	}
}
