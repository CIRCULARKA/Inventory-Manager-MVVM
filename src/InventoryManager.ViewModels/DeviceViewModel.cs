using InventoryManager.Commands;
using InventoryManager.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System;
using InventoryManager.Views;
using InventoryManager.Extensions;

namespace InventoryManager.ViewModels
{
	public class DeviceViewModel : ViewModelBase
	{
		private string _inputtedInventoryNumber;

		private string _inputtedNetworkName;

		private string _inputtedIPAddress;

		private string _inputtedDeviceDeviceAccountName;

		private string _inputtedDevicePassword;

		private Device _selectedDevice;

		private Housing _selectedHousing;

		private Cabinet _selectedCabinet;

		private ObservableCollection<DeviceAccount> _selectedDeviceAccounts;

		private ObservableCollection<IPAddress> _selectedDeviceIPAddresses;

		private ObservableCollection<Device> _allDevices;

		private List<Cabinet> _selectedHousingCabinets;

		private List<Housing> _allHousings;

		private List<Cabinet> _allCabinets;

		public DeviceViewModel()
		{
			// Load devices housings and cabinets explicitly because device must have them from
			// _allHousings and _allCabinets instances so SelectedHousing and SelectedCabinet bindings will work
			_allHousings = Locations.AllHousings as List<Housing>;
			_allCabinets = Locations.AllCabinets as List<Cabinet>;
			_allDevices = Devices.All.ToObservableCollection();
			foreach (var device in _allDevices)
			{
				device.Cabinet = _allCabinets.Find(c => c.ID == device.CabinetID);
				device.Cabinet.Housing = _allHousings.Find(h => h.ID == device.Cabinet.HousingID);
			}

			ShowDeviceMovementHistoryCommand = new ButtonCommand(
				(obj) =>
				{
					var deviceHistoryView = new DeviceMovementHistoryView();
					deviceHistoryView.DataContext = this;
					deviceHistoryView.Title = $"История перемещений {SelectedDevice.InventoryNumber}";
					deviceHistoryView.ShowDialog();
				},
				(obj) => SelectedDevice != null
			);

			OpenAddDeviceViewCommand = new ButtonCommand(
				(obj) =>
				{
					var addDeviceWindow = new AddDeviceView();
					addDeviceWindow.DataContext = this;
					addDeviceWindow.ShowDialog();
				}
			);

			AddDeviceCommand = new ButtonCommand(
				(obj) =>
				{
					var newDevice = new Device
					{
						InventoryNumber = InputtedInventoryNumber,
						DeviceTypeID = SelectedDeviceType.ID,
						NetworkName = InputtedNetworkName,
					};

					try
					{
						Devices.Add(newDevice);
						Devices.SaveChanges();

						// Add DeviceType and Cabinet object explicitly in order to avoid db future exceptions
						newDevice.DeviceType = SelectedDeviceType;
						newDevice.Cabinet = Locations.Find(newDevice.CabinetID);
						newDevice.Cabinet.Housing = _allHousings.Find(h => h.ID == newDevice.Cabinet.HousingID);
						AllDevices.Add(newDevice);

						InputtedInventoryNumber = "";
						InputtedNetworkName = "";
						MessageToUser = "Устройство добавлено";
					}
					catch (Exception e)
					{
						MessageToUser = e.Message;
						Devices.Remove(newDevice);
					}
				},
				(obj) =>
				{
					return !string.IsNullOrEmpty(InputtedInventoryNumber) &&
						!string.IsNullOrEmpty(InputtedNetworkName) &&
						SelectedDeviceType != null;
				}
			);

			RemoveDeviceCommand = new ButtonCommand(
				(obj) =>
				{
					Devices.Remove(SelectedDevice);
					Devices.SaveChanges();
					AllDevices.Remove(SelectedDevice);
				},
				(obj) => SelectedDevice != null
			);

			ShowAddDeviceAccountViewCommand = new ButtonCommand(
				(obj) =>
				{
					var addAccountView = new AddDeviceAccountView();
					addAccountView.DataContext = this;
					addAccountView.ShowDialog();
				},
				(obj) => SelectedDevice != null && SelectedDevice?.DeviceType.Name != "Коммутатор"
			);

			AddDeviceAccountCommand = new ButtonCommand(
				(obj) =>
				{
					var newAcc = new DeviceAccount
					{
						DeviceID = SelectedDevice.ID,
						Login = InputtedDeviceAccountLogin,
						Password = InputtedDeviceAccountPassword
					};

					try
					{
						Devices.AddDeviceAccount(newAcc);
						Devices.SaveChanges();

						SelectedDeviceAccounts.Add(newAcc);

						InputtedDeviceAccountLogin = "";
						InputtedDeviceAccountPassword = "";

						MessageToUser = "Учётная запись успешно добавлена";
					}
					catch (Exception)
					{
						MessageToUser = "Учётная запись с таким логином уже существует";
						Devices.RemoveDeviceAccount(newAcc);
					}
				},
				(obj) => !(string.IsNullOrWhiteSpace(InputtedDeviceAccountLogin) ||
					string.IsNullOrWhiteSpace(InputtedDeviceAccountPassword))
			);

			RemoveDeviceAccountCommand = new ButtonCommand(
				(obj) =>
				{
					Devices.RemoveDeviceAccount(SelectedDeviceAccount);
					Devices.SaveChanges();

					SelectedDeviceAccounts.Remove(SelectedDeviceAccount);
				},
				(obj) => SelectedDeviceAccount != null
			);

			ShowAddIPViewCommand = new ButtonCommand(
				(obj) =>
				{
					var addIpView = new AddIPAddressView();
					addIpView.DataContext = this;
					addIpView.ShowDialog();
				},
				(obj) => SelectedDevice != null
			);

			AddDeviceIPCommand = new ButtonCommand(
				(obj) =>
				{
					var newIP = new IPAddress
					{
						Address = InputtedIPAddress,
						DeviceID = SelectedDevice.ID
					};

					try
					{
						Devices.AddIPAddress(newIP);
						Devices.SaveChanges();
						SelectedDeviceIPAddresses.Add(newIP);

						InputtedIPAddress = "";
						MessageToUser = "Адрес успешно добавлен";
					}
					catch (Exception)
					{
						MessageToUser = "Такой адрес уже используется";
						Devices.RemoveIPAddress(newIP);
					}
				}
			);

			RemoveDeviceIPCommand = new ButtonCommand(
				(obj) =>
				{
					Devices.RemoveIPAddress(SelectedDeviceIP);
					Devices.SaveChanges();

					SelectedDeviceIPAddresses.Remove(SelectedDeviceIP);
				},
				(obj) => SelectedDeviceIP != null
			);

			ApplyDeviceLocationChangesCommand = new ButtonCommand(
				(obj) =>
				{
					SelectedDevice.Cabinet = SelectedCabinet;
					SelectedDevice.Cabinet.Housing = SelectedHousing;

					try
					{
						Devices.Update(SelectedDevice);

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
							Devices.NoteDeviceMovemet(newRecord);
							Devices.SaveChanges();
						}
						catch
						{
							Devices.RemoveDeviceMovementNote(newRecord);
						}

						Devices.SaveChanges();
					}
					catch (Exception e)
					{
						MessageToUser = e.Message;
					}
				}
			);
		}

		public ObservableCollection<Device> AllDevices =>
			_allDevices;

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

		public List<DeviceMovementHistoryNote> SelectedDeviceMovementHistoryNote =>
			Devices.GetAllDeviceHistoryNotes(SelectedDevice) as List<DeviceMovementHistoryNote>;

		public List<DeviceType> AllDeviceTypes =>
			Devices.AllDeviceTypes as List<DeviceType>;

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
					// Getting all device's accounts
					SelectedDeviceAccounts = Devices.AllDeviceAccounts.
						Where(a => a.DeviceID == SelectedDevice.ID).
						ToObservableCollection();

					// Getting all device's IP's
					SelectedDeviceIPAddresses = Devices.GetAllDeviceIPAddresses(SelectedDevice).
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

		public DeviceAccount SelectedDeviceAccount { get; set; }

		public IPAddress SelectedDeviceIP { get; set; }

		public DeviceType SelectedDeviceType { get; set; }

		public ButtonCommand ShowDeviceMovementHistoryCommand { get; set; }

		public ButtonCommand AddDeviceCommand { get; }

		public ButtonCommand RemoveDeviceCommand { get; }

		public ButtonCommand OpenAddDeviceViewCommand { get; }

		public ButtonCommand ShowAddDeviceAccountViewCommand { get; }

		public ButtonCommand ShowAddIPViewCommand { get; set; }

		public ButtonCommand AddDeviceIPCommand { get; }

		public ButtonCommand RemoveDeviceIPCommand { get; }

		public ButtonCommand AddDeviceAccountCommand { get; }

		public ButtonCommand RemoveDeviceAccountCommand { get; }

		public ButtonCommand ApplyDeviceLocationChangesCommand { get; }

		public string InputtedInventoryNumber
		{
			get => _inputtedInventoryNumber;
			set
			{
				_inputtedInventoryNumber = value;
				OnPropertyChanged("InputtedInventoryNumber");
			}
		}

		public string InputtedNetworkName
		{
			get => _inputtedNetworkName;
			set
			{
				_inputtedNetworkName = value;
				OnPropertyChanged("InputtedNetworkName");
			}
		}

		public string InputtedDeviceAccountLogin
		{
			get => _inputtedDeviceDeviceAccountName;
			set
			{
				_inputtedDeviceDeviceAccountName = value;
				OnPropertyChanged("InputtedDeviceDeviceAccountName");
			}
		}

		public string InputtedDeviceAccountPassword
		{
			get => _inputtedDevicePassword;
			set
			{
				_inputtedDevicePassword = value;
				OnPropertyChanged("InputtedDevicePassword");
			}
		}

		public string InputtedIPAddress
		{
			get => _inputtedIPAddress;
			set
			{
				_inputtedIPAddress = value;
				OnPropertyChanged(nameof(InputtedIPAddress));
			}
		}
	}
}
