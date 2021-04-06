using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Events;
using InventoryManager.Commands;
using InventoryManager.Extensions;
using InventoryManager.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InventoryManager.ViewModels
{
	public class DevicesListViewModel : ViewModelBase
	{
		private Device _selectedDevice;

		private ObservableCollection<Device> _filteredDevices;

		public DevicesListViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			AllDevices = Repository.AllDevices.ToList();

			InitDevicesLocationWithInstances();

			FilterDevicesAccordingToCriteria();

			ShowAddDeviceViewCommand = RegisterCommandAction(
				(obj) => AddDeviceView.ShowDialog(),
				(obj) =>
					UserSession.IsAuthorizedUserAllowedTo(UserActions.AddDevice)
			);

			RemoveDeviceCommand = RegisterCommandAction(
				(obj) =>
				{
					Repository.RemoveDevice(SelectedDevice);

					Repository.DeleteAllDeviceMovementHistory(SelectedDevice);

					Repository.SaveChanges();
					AllDevices.Remove(AllDevices.Find(d => d.ID == SelectedDevice.ID));
					FilteredDevices.Remove(SelectedDevice);
				},
				(obj) =>
				{
					if (UserSession.IsAuthorizedUserAllowedTo(UserActions.RemoveDevice))
						return SelectedDevice != null;
					else return false;
				}
			);

			SubscribeActionOnDeviceAddition(
				(device) =>
				{
					device.DeviceType = Repository.AllDeviceTypes.Single(dt => dt.ID == device.DeviceTypeID);
					device.Cabinet = Repository.FindCabinet(device.CabinetID);
					device.Cabinet.Housing = DeviceLocationViewModel.
						AllHousings.
							First(h => h.ID == device.Cabinet.HousingID);

					AllDevices.Add(device);
					if (DevicesFilter.IsDeviceMeetsSearchAndFilteringCriteria(device))
						FilteredDevices.Add(device);
				}
			);

			SubscribeActionOnFilteringCriteraChanges(
				(filteredDevices) =>
					FilteredDevices = filteredDevices.ToObservableCollection()
			);

			DeviceEvents.OnDeviceSelectionChanged += (d) =>
			{
				if (d != null) EnableDeviceLocationChanges();
				else
				{
					ClearDeviceAccountsList();
					ClearDeviceIPList();
					ClearDeviceLocationLists();
					DisableDeviceLocationChanges();
				}
			};
		}

		private IDeviceRelatedRepository Repository { get; set; }

		public ObservableCollection<Device> FilteredDevices
		{
			get => _filteredDevices;
			set
			{
				_filteredDevices = value;
				OnPropertyChanged(nameof(FilteredDevices));
			}
		}

		public DeviceFilter DevicesFilter =>
			ViewModelLinker.GetRegisteredViewModel<DeviceSearchAndFilteringViewModel>().DevicesFilter;

		public List<Device> AllDevices { get; }

		public Device SelectedDevice
		{
			get => _selectedDevice;
			set
			{
				_selectedDevice = value;

				DeviceEvents.
					RaiseOnDeviceSelectionChanged(_selectedDevice);
			}
		}

		public Command ShowAddDeviceViewCommand { get; }

		public Command RemoveDeviceCommand { get; }

		public DeviceLocationViewModel DeviceLocationViewModel =>
			ViewModelLinker.
				GetRegisteredViewModel<DeviceLocationViewModel>();

		public AddDeviceView AddDeviceView =>
			ViewModelLinker.GetRegisteredView<AddDeviceView>();

		private void EnableDeviceLocationChanges() =>
			ViewModelLinker.
				GetRegisteredViewModel<DeviceLocationViewModel>().
					IsDeviceLocationChoosingAvailable = true;

		private void DisableDeviceLocationChanges() =>
			ViewModelLinker.
				GetRegisteredViewModel<DeviceLocationViewModel>().
					IsDeviceLocationChoosingAvailable = false;

		private void ClearDeviceLocationLists()
		{
			var locationVm = ViewModelLinker.
				GetRegisteredViewModel<DeviceLocationViewModel>();

			locationVm.SelectedCabinet = null;
			locationVm.SelectedHousing = null;
		}

		private void ClearDeviceAccountsList() =>
			ViewModelLinker.
				GetRegisteredViewModel<DeviceAccountsListViewModel>().
					SelectedDeviceAccounts = null;

		private void ClearDeviceIPList() =>
			ViewModelLinker.
				GetRegisteredViewModel<DeviceIPListViewModel>().
					SelectedDeviceIPAddresses = null;

		private void InitDevicesLocationWithInstances()
		{
			foreach (var device in AllDevices)
			{
				device.Cabinet = DeviceLocationViewModel.
					AllCabinets.
						First(c => c.ID == device.CabinetID);

				device.Cabinet.Housing = DeviceLocationViewModel.
					AllHousings.
						First(h => h.ID == device.Cabinet.HousingID);
			}
		}
		private void SubscribeActionOnDeviceAddition(Action<Device> action) =>
			DeviceEvents.OnNewDeviceAdded += action;

		private void SubscribeActionOnFilteringCriteraChanges(Action<IEnumerable<Device>> action) =>
			DeviceEvents.OnDeviceFilteringCriteriaChanged += action;

		private void FilterDevicesAccordingToCriteria() =>
			FilteredDevices = DevicesFilter.
				GetFilteredDevicesList(AllDevices).
					ToObservableCollection();
	}
}
