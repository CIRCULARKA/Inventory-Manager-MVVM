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
	public class DevicesListViewModel : ViewModelBase, IDevicesListViewModel, IUserSessionViewModel
	{
		private Device _selectedDevice;

		private ObservableCollection<Device> _filteredDevices;

		public DevicesListViewModel(IDeviceRelatedRepository repo, IUserSession userSession)
		{
			Repository = repo;

			UserSession = userSession;

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
					if (DevicesFilter.DoesMeetSearchingAndFilteringCriteria(device))
						FilteredDevices.Add(device);
				}
			);

			SubscribeActionOnFilteringCriteraChanges(
				(filteredDevices) =>
					FilteredDevices = filteredDevices.ToObservableCollection()
			);
		}

		private IDeviceRelatedRepository Repository { get; set; }

		public IUserSession UserSession { get; }

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
			(ResolveDependency<IDeviceSearchAndFilteringViewModel>() as DeviceSearchAndFilteringViewModel).
				DevicesFilter;

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
			(ResolveDependency<IDeviceLocationViewModel>() as DeviceLocationViewModel);

		public AddDeviceView AddDeviceView { get; set; }

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
				Filter(AllDevices).
					ToObservableCollection();
	}
}
