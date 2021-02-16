using InventoryManager.Commands;
using InventoryManager.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
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
			_allHousings = HousingModel.All();
			_allCabinets = CabinetModel.All();
			_allDevices = DeviceModel.All().ToObservableCollection();
			foreach (var device in _allDevices)
			{
				device.Cabinet = _allCabinets.Find(c => c.ID == device.CabinetID);
				device.Cabinet.Housing = _allHousings.Find(h => h.ID == device.Cabinet.HousingID);
			}

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
						DeviceModel.Add(newDevice);
						DeviceModel.SaveChanges();

						// Add DeviceType and Cabinet object explicitly in order to avoid db future exceptions
						newDevice.DeviceType = SelectedDeviceType;
						newDevice.Cabinet = CabinetModel.Find(newDevice.CabinetID);
						newDevice.Cabinet.Housing = _allHousings.Find(h => h.ID == newDevice.Cabinet.HousingID);
						AllDevices.Add(newDevice);

						InputtedInventoryNumber = "";
						InputtedNetworkName = "";
						MessageToUser = "Устройство добавлено";
					}
					catch (System.Exception e)
					{
						MessageToUser = e.Message;
						DeviceModel.Remove(newDevice);
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
					DeviceModel.Remove(SelectedDevice);
					DeviceModel.SaveChanges();
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
						DeviceAccountModel.Add(newAcc);
						DeviceAccountModel.SaveChanges();
						SelectedDeviceAccounts.Add(newAcc);

						InputtedDeviceAccountLogin = "";
						InputtedDeviceAccountPassword = "";

						MessageToUser = "Учётная запись успешно добавлена";
					}
					catch (System.Exception)
					{
						MessageToUser = "Учётная запись с таким логином уже существует";
						DeviceAccountModel.Remove(newAcc);
					}
				},
				(obj) => !(string.IsNullOrWhiteSpace(InputtedDeviceAccountLogin) ||
					string.IsNullOrWhiteSpace(InputtedDeviceAccountPassword))
			);

			RemoveDeviceAccountCommand = new ButtonCommand(
				(obj) =>
				{
					DeviceAccountModel.Remove(SelectedDeviceAccount);
					DeviceAccountModel.SaveChanges();

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
						IPAddressModel.Add(newIP);
						IPAddressModel.SaveChanges();
						SelectedDeviceIPAddresses.Add(newIP);

						InputtedIPAddress = "";
						MessageToUser = "Адрес успешно добавлен";
					}
					catch (System.Exception e)
					{
						MessageToUser = "Такой адрес уже используется";
						IPAddressModel.Remove(newIP);
					}
				}
			);

			RemoveDeviceIPCommand = new ButtonCommand(
				(obj) =>
				{
					IPAddressModel.Remove(SelectedDeviceIP);
					IPAddressModel.SaveChanges();

					SelectedDeviceIPAddresses.Remove(SelectedDeviceIP);
				},
				(obj) => SelectedDeviceIP != null
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

		public List<DeviceType> AllDeviceTypes =>
			DeviceTypeModel.All();

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
					SelectedDeviceAccounts = DeviceAccountModel.All().
						Where(a => a.DeviceID == SelectedDevice.ID).
						ToObservableCollection();

					// Getting all device's IP's
					SelectedDeviceIPAddresses = IPAddressModel.All().
						Where(ip => ip.DeviceID == SelectedDevice.ID).
						ToObservableCollection();

					// Getting device's housing
					SelectedHousing = SelectedDevice.Cabinet.Housing;

					// Select device's cabinet
					SelectedCabinet = SelectedDevice.Cabinet;
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

		public ButtonCommand AddDeviceCommand { get; }

		public ButtonCommand RemoveDeviceCommand { get; }

		public ButtonCommand OpenAddDeviceViewCommand { get; }

		public ButtonCommand ShowAddDeviceAccountViewCommand { get; }

		public ButtonCommand ShowAddIPViewCommand { get; set; }

		public ButtonCommand AddDeviceIPCommand { get; }

		public ButtonCommand RemoveDeviceIPCommand { get; }

		public ButtonCommand AddDeviceAccountCommand { get; }

		public ButtonCommand RemoveDeviceAccountCommand { get; }

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
