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

		private string _inputtedSearchQuery;

		private Device _selectedDevice;

		private Housing _selectedHousing;

		private Cabinet _selectedCabinet;

		private ObservableCollection<DeviceAccount> _selectedDeviceAccounts;

		private ObservableCollection<IPAddress> _selectedDeviceIPAddresses;

		private ObservableCollection<Device> _devicesToShow;

		private List<Cabinet> _selectedHousingCabinets;

		private List<Housing> _allHousings;

		private List<Cabinet> _allCabinets;

		private IEnumerable<DeviceMovementHistoryNote> _allDeviceHistoryNotes;

		private DeviceFilter _deviceFilter;

		public DeviceViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			// Load devices housings and cabinets explicitly because device must have them from
			// _allHousings and _allCabinets instances so SelectedHousing and SelectedCabinet bindings will work
			_allHousings = Repository.AllHousings.ToList();
			_allCabinets = Repository.AllCabinets.ToList();
			AllDevices = Repository.AllDevices.ToList();

			_deviceFilter = new DeviceFilter();

			InitDevicesLocationWithInstances();

			DevicesToShow = _deviceFilter.
				GetFilteredDevicesList(AllDevices).
				ToObservableCollection();

			SubscribeActionOnDeviceAddition(
				(device) =>
				{
					device.DeviceType = Repository.AllDeviceTypes.Single(dt => dt.ID == device.DeviceTypeID);
					device.Cabinet = Repository.FindCabinet(device.CabinetID);
					device.Cabinet.Housing = _allHousings.Find(h => h.ID == device.Cabinet.HousingID);

					AllDevices.Add(device);
					if (_deviceFilter.IsDeviceMeetsSearchAndFilteringCriteria(device))
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

			FilterDevicesCommand = RegisterCommandAction(
				(obj) =>
				{
					_deviceFilter.IncludeServers = IsServersIncluded;
					_deviceFilter.IncludeSwitches = IsSwitchesIncluded;
					_deviceFilter.IncludePC = IsPCIncluded;

					DevicesToShow = _deviceFilter.
						GetFilteredDevicesList(AllDevices).
						ToObservableCollection();
				}
			);
		}

		private IDeviceRelatedRepository Repository { get; }

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

		public ObservableCollection<Device> DevicesToShow
		{
			get => _devicesToShow;
			set
			{
				_devicesToShow = value;
				OnPropertyChanged(nameof(DevicesToShow));
			}
		}

		public List<Device> AllDevices { get; set; }

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

		public string InputtedSearchQuery
		{
			get => _inputtedSearchQuery;
			set
			{
				_inputtedSearchQuery = value;
				_deviceFilter.SearchQuery = value;
				DevicesToShow = _deviceFilter.
					GetFilteredDevicesList(AllDevices).
					ToObservableCollection();
			}
		}

		public bool IsServersIncluded { get; set; } = true;

		public bool IsPCIncluded { get; set; } = true;

		public bool IsSwitchesIncluded { get; set; } = true;

		public DeviceAccount SelectedDeviceAccount { get; set; }

		public IPAddress SelectedDeviceIP { get; set; }

		public Command ShowDeviceMovementHistoryCommand { get; set; }

		public Command ShowAddDeviceAccountViewCommand { get; }

		public Command ShowAddIPViewCommand { get; set; }

		public Command RemoveDeviceIPCommand { get; }

		public Command RemoveDeviceAccountCommand { get; }

		public Command ApplyDeviceLocationChangesCommand { get; }

		public Command FilterDevicesCommand { get; }

		private void SubscribeActionOnDeviceAddition(Action<Device> action) =>
			AddDeviceViewModel.OnDeviceAdded += action;

		private void SubscribeActionOnIPAddition(Action<IPAddress> action) =>
			DeviceIPViewModel.OnIPAssigned += action;

		private void SubscribeActionOnDeviceAccountAddition(Action<DeviceAccount> action) =>
			DeviceAccountViewModel.OnDeviceAccountAdded += action;

		private void SubscribeActionOnNetworkMaskChanges(Action action) =>
			ViewModelLinker.GetRegisteredViewModel<ConfigureIPSettingsViewModel>()
				.OnNetworkMaskChanged += action;

		private void ClearDevicesIPLists() =>
			SelectedDeviceIPAddresses?.Clear();

		private void InitDevicesLocationWithInstances()
		{
			foreach (var device in AllDevices)
			{
				device.Cabinet = _allCabinets.Find(c => c.ID == device.CabinetID);
				device.Cabinet.Housing = _allHousings.Find(h => h.ID == device.Cabinet.HousingID);
			}
		}

		private void RefreshSelectedDeviceHistory() =>
			SelectedDeviceMovementHistoryNotes =
				Repository.GetAllDeviceHistoryNotes(SelectedDevice).ToList();

		private void AddNewDeviceToInitialList(Device newDevice) =>
			_deviceFilter.InitialList.Add(newDevice);
	}
}
