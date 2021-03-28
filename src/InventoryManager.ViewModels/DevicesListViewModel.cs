using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Views;
using InventoryManager.Infrastructure;
using InventoryManager.Extensions;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System;

namespace InventoryManager.ViewModels
{
	public class DevicesListViewModel : ViewModelBase
	{
		private List<Housing> _allHousings;

		private List<Cabinet> _allCabinets;

		private Device _selectedDevice;

		public DevicesListViewModel(IDeviceRelatedRepository repo, DeviceFilter filter)
		{
			Repository = repo;

			_allHousings = Repository.AllHousings.ToList();
			_allCabinets = Repository.AllCabinets.ToList();
			AllDevices = Repository.AllDevices.ToList();

			InitDevicesLocationWithInstances();

			DevicesFilter = filter;

			FilteredDevices = DevicesFilter.
				GetFilteredDevicesList(AllDevices).
				ToObservableCollection();

			ShowAddDeviceViewCommand = RegisterCommandAction(
				(obj) => AddDeviceView.ShowDialog()

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
				(obj) => SelectedDevice != null
			);

			SubscribeActionOnDeviceAddition(
				(device) =>
				{
					device.DeviceType = Repository.AllDeviceTypes.Single(dt => dt.ID == device.DeviceTypeID);
					device.Cabinet = Repository.FindCabinet(device.CabinetID);
					device.Cabinet.Housing = _allHousings.Find(h => h.ID == device.Cabinet.HousingID);

					AllDevices.Add(device);
					if (DevicesFilter.IsDeviceMeetsSearchAndFilteringCriteria(device))
						FilteredDevices.Add(device);
				}
			);
		}

		private IDeviceRelatedRepository Repository { get; set; }

		public event Action<Device> SelectedDeviceChanged;

		public ObservableCollection<Device> FilteredDevices { get; set; }

		public DeviceFilter DevicesFilter { get; }

		public List<Device> AllDevices { get; }

		public Device SelectedDevice
		{
			get => _selectedDevice;
			set
			{
				_selectedDevice = value;

				SelectedDeviceChanged?.Invoke(_selectedDevice);
			}
		}

		public Command ShowAddDeviceViewCommand{ get; }

		public Command RemoveDeviceCommand { get; }

		public AddDeviceView AddDeviceView =>
			ViewModelLinker.GetRegisteredView<AddDeviceView>();

		private void InitDevicesLocationWithInstances()
		{
			foreach (var device in AllDevices)
			{
				device.Cabinet = _allCabinets.Find(c => c.ID == device.CabinetID);
				device.Cabinet.Housing = _allHousings.Find(h => h.ID == device.Cabinet.HousingID);
			}
		}
		private void SubscribeActionOnDeviceAddition(Action<Device> action) =>
			ViewModelLinker.
				GetRegisteredViewModel<AddDeviceViewModel>().
					OnDeviceAdded += action;
	}
}
