using InventoryManager.Commands;
using InventoryManager.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System;
using InventoryManager.Views;
using InventoryManager.Extensions;
using InventoryManager.Infrastructure;

namespace InventoryManager.ViewModels
{
	public class DeviceViewModel : ViewModelBase
	{
		private bool _isDeviceLocationChoosingAvailable;

		private Device _selectedDevice;

		private Housing _selectedHousing;

		private Cabinet _selectedCabinet;

		private ObservableCollection<DeviceAccount> _selectedDeviceAccounts;

		private ObservableCollection<IPAddress> _selectedDeviceIPAddresses;

		private ObservableCollection<Device> _allDevices;

		private List<Cabinet> _selectedHousingCabinets;

		private List<Housing> _allHousings;

		private List<Cabinet> _allCabinets;

		private IEnumerable<DeviceMovementHistoryNote> _allDeviceHistoryNotes;

		public DeviceViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			// Load devices housings and cabinets explicitly because device must have them from
			// _allHousings and _allCabinets instances so SelectedHousing and SelectedCabinet bindings will work
			_allHousings = Repository.AllHousings.ToList();
			_allCabinets = Repository.AllCabinets.ToList();
			_allDevices = Repository.AllDevices.ToObservableCollection();

			InitDevicesLocationWithInstances();

			SubscribeActionOnDeviceAddition(
				(device) =>
				{
					device.DeviceType = Repository.AllDeviceTypes.Single(dt => dt.ID == device.DeviceTypeID);
					device.Cabinet = Repository.FindCabinet(device.CabinetID);
					device.Cabinet.Housing = _allHousings.Find(h => h.ID == device.Cabinet.HousingID);
					DevicesToShow.Add(device);
				}
			);

			SubscribeActionOnIPAddition(
				(ipAddress) => SelectedDeviceIPAddresses.Add(ipAddress)
			);

			SubscribeActionOnDeviceAccountAddition(
				(newAcc) => SelectedDeviceAccounts.Add(newAcc)
			);

			SubscribeActionOnNetworkMaskChanges(ClearDevicesIPLists);

			ShowDeviceMovementHistoryCommand = RegisterCommandAction(
				(obj) =>
				{
					RefreshSelectedDeviceHistory();
					DeviceMovementHistoryView.Title = $"История перемещений {SelectedDevice.InventoryNumber}";
					DeviceMovementHistoryView.ShowDialog();
				},
				(obj) => SelectedDevice != null
			);

			OpenAddDeviceViewCommand = RegisterCommandAction(
				(obj) => AddDeviceView.ShowDialog()
			);

			RemoveDeviceCommand = RegisterCommandAction(
				(obj) =>
				{
					Repository.RemoveDevice(SelectedDevice);

					// Delete also all device movement history
					Repository.DeleteAllDeviceMovementHistory(SelectedDevice);

					Repository.SaveChanges();
					DevicesToShow.Remove(SelectedDevice);
				},
				(obj) => SelectedDevice != null
			);

			ShowAddDeviceAccountViewCommand = RegisterCommandAction(
				(obj) =>
				{
					DeviceAccountViewModel.TargetDevice = SelectedDevice;
					AddDeviceAccountView.ShowDialog();
				},
				(obj) => SelectedDevice != null && SelectedDevice?.DeviceType?.Name != "Коммутатор"
			);

			RemoveDeviceAccountCommand = RegisterCommandAction(
				(obj) =>
				{
					DeviceAccountViewModel.RemoveAccountFromDevice(SelectedDeviceAccount);
					SelectedDeviceAccounts.Remove(SelectedDeviceAccount);
				},
				(obj) => SelectedDeviceAccount != null
			);

			ShowAddIPViewCommand = RegisterCommandAction(
				(obj) =>
				{
					DeviceIPViewModel.TargetDevice = SelectedDevice;
					AddIPAddressView.ShowDialog();
				},
				(obj) => SelectedDevice != null
			);

			RemoveDeviceIPCommand = RegisterCommandAction(
				(obj) =>
				{
					DeviceIPViewModel.RemoveIPAddress(SelectedDeviceIP);
					SelectedDeviceIPAddresses.Remove(SelectedDeviceIP);
				},
				(obj) => SelectedDeviceIP != null
			);

			ApplyDeviceLocationChangesCommand = RegisterCommandAction(
				(obj) =>
				{
					SelectedDevice.Cabinet = SelectedCabinet;
					SelectedDevice.Cabinet.Housing = SelectedHousing;

					try
					{
						Repository.UpdateDevice(SelectedDevice);

						var newRecord = new DeviceMovementHistoryNote()
						{
							DeviceID = SelectedDevice.ID,
							TargetCabinetID = SelectedDevice.Cabinet.ID,
							Date = DateTime.Now,
							// Reason field is temporary. Need to create entity with reasons
							// and use it instead of this hard coding
							Reason = "Перемещение"
						};

						try
						{
							Repository.FixDeviceMovement(newRecord);
							Repository.SaveChanges();
						}
						catch
						{
							Repository.RemoveDeviceMovementNote(newRecord);
						}

						Repository.SaveChanges();
					}
					catch (Exception e)
					{
						MessageToUser = e.Message;
					}
				},
				(obj) => SelectedDevice != null
			);
		}

		private IDeviceRelatedRepository Repository { get; }

		public ObservableCollection<Device> DevicesToShow =>
			_allDevices;

		public AddDeviceView AddDeviceView =>
			ViewModelLinker.
				GetRegisteredView<AddDeviceView>();

		public AddDeviceViewModel AddDeviceViewModel =>
			ViewModelLinker.
				GetRegisteredViewModel<AddDeviceViewModel>();

		public AddIPAddressView AddIPAddressView =>
			ViewModelLinker.
				GetRegisteredView<AddIPAddressView>();

		public DeviceIPViewModel DeviceIPViewModel =>
			ViewModelLinker.
				GetRegisteredViewModel<DeviceIPViewModel>();

		public AddDeviceAccountView AddDeviceAccountView =>
			ViewModelLinker.
				GetRegisteredView<AddDeviceAccountView>();

		public DeviceMovementHistoryView DeviceMovementHistoryView =>
			ViewModelLinker.
				GetRegisteredView<DeviceMovementHistoryView>();

		public DeviceAccountViewModel DeviceAccountViewModel =>
			ViewModelLinker.
				GetRegisteredViewModel<DeviceAccountViewModel>();

		public ObservableCollection<IPAddress> SelectedDeviceIPAddresses
		{
			get => _selectedDeviceIPAddresses;
			set
			{
				_selectedDeviceIPAddresses = value;
				OnPropertyChanged("SelectedDeviceIPAddresses");
			}
		}

		public ObservableCollection<DeviceAccount> SelectedDeviceAccounts
		{
			get => _selectedDeviceAccounts;
			set
			{
				_selectedDeviceAccounts = value;
				OnPropertyChanged("SelectedDeviceAccounts");
			}
		}

		public IEnumerable<DeviceMovementHistoryNote> SelectedDeviceMovementHistoryNotes
		{
			get => _allDeviceHistoryNotes;
			set
			{
				_allDeviceHistoryNotes = value;
				OnPropertyChanged(nameof(SelectedDeviceMovementHistoryNotes));
			}
		}

		public List<Housing> AllHousings => _allHousings;

		public List<Cabinet> SelectedHousingCabinets
		{
			get => _selectedHousingCabinets;
			set
			{
				_selectedHousingCabinets = value;
				OnPropertyChanged("SelectedHousingCabinets");
			}
		}

		public Device SelectedDevice
		{
			get => _selectedDevice;
			set
			{
				_selectedDevice = value;

				if (SelectedDevice != null)
				{
					// Enabling possibility to chose device location
					IsDeviceLocationChoosingAvailable = true;

					// Getting all device's accounts
					SelectedDeviceAccounts = Repository.GetAllDeviceAccounts(SelectedDevice).
						ToObservableCollection();

					// Getting all device's IP's
					SelectedDeviceIPAddresses = Repository.GetAllDeviceIPAddresses(SelectedDevice).
						ToObservableCollection();

					// Getting device's housing
					SelectedHousing = SelectedDevice.Cabinet.Housing;
				}
				else
				{
					SelectedDeviceAccounts = null;
					SelectedDeviceIPAddresses = null;
					SelectedHousing = null;
					SelectedCabinet = null;
					IsDeviceLocationChoosingAvailable = false;
				}

				OnPropertyChanged("SelectedDevice");
			}
		}

		public Housing SelectedHousing
		{
			get => _selectedHousing;
			set
			{
				_selectedHousing = value;
				if (_selectedHousing != null)
				{
					SelectedHousingCabinets = _allCabinets.
						Where(c => c.HousingID == _selectedHousing.ID).
						ToList();

					if (SelectedDevice.Cabinet.HousingID == SelectedHousing.ID)
						SelectedCabinet = SelectedDevice?.Cabinet;
					else
						SelectedCabinet = _allCabinets.First(c => c.HousingID == SelectedHousing.ID);
				}
				else SelectedHousingCabinets = null;
				OnPropertyChanged("SelectedHousing");
			}
		}

		public Cabinet SelectedCabinet
		{
			get => _selectedCabinet;
			set
			{
				_selectedCabinet = value;
				OnPropertyChanged("SelectedCabinet");
			}
		}

		public bool IsDeviceLocationChoosingAvailable
		{
			get => _isDeviceLocationChoosingAvailable;
			set
			{
				_isDeviceLocationChoosingAvailable = value;
				OnPropertyChanged(nameof(IsDeviceLocationChoosingAvailable));
			}
		}

		public DeviceAccount SelectedDeviceAccount { get; set; }

		public IPAddress SelectedDeviceIP { get; set; }

		public Command ShowDeviceMovementHistoryCommand { get; set; }

		public Command RemoveDeviceCommand { get; }

		public Command OpenAddDeviceViewCommand { get; }

		public Command ShowAddDeviceAccountViewCommand { get; }

		public Command ShowAddIPViewCommand { get; set; }

		public Command RemoveDeviceIPCommand { get; }

		public Command RemoveDeviceAccountCommand { get; }

		public Command ApplyDeviceLocationChangesCommand { get; }

		private void SubscribeActionOnDeviceAddition(Action<Device> action) =>
			AddDeviceViewModel.OnDeviceAdded += action;

		private void SubscribeActionOnIPAddition(Action<IPAddress> action) =>
			DeviceIPViewModel.OnIPAssigned += action;

		private void SubscribeActionOnDeviceAccountAddition(Action<DeviceAccount> action) =>
			DeviceAccountViewModel.OnDeviceAccountAdded += action;

		private void SubscribeActionOnNetworkMaskChanges(Action action)	=>
			ViewModelLinker.GetRegisteredViewModel<ConfigureIPSettingsViewModel>()
				.OnNetworkMaskChanged += action;

		private void ClearDevicesIPLists() =>
			SelectedDeviceIPAddresses?.Clear();

		private void InitDevicesLocationWithInstances()
		{
			foreach (var device in _allDevices)
			{
				device.Cabinet = _allCabinets.Find(c => c.ID == device.CabinetID);
				device.Cabinet.Housing = _allHousings.Find(h => h.ID == device.Cabinet.HousingID);
			}
		}

		private void RefreshSelectedDeviceHistory() =>
			SelectedDeviceMovementHistoryNotes =
				Repository.GetAllDeviceHistoryNotes(SelectedDevice).ToList();

	}
}
