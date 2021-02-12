using InventoryManager.Commands;
using InventoryManager.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using InventoryManager.Views;
using InventoryManager.Extensions;
using System.Windows.Controls;

namespace InventoryManager.ViewModels
{
	public class DeviceViewModel : ViewModelBase
	{
		private string _inputtedInventoryNumber;

		private string _inputtedNetworkName;

		private string _inputtedDeviceDeviceAccountName;

		private string _inputtedDevicePassword;

		private int _selectedHousingIndex;

		private Device _selectedDevice;

		private Housing _selectedHousing;

		private Cabinet _selectedCabinet;

		private ObservableCollection<DeviceAccount> _selectedDeviceDeviceAccounts;

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

						// Add DeviceType explicitly in order to avoid db exception
						newDevice.DeviceType = SelectedDeviceType;
						AllDevices.Add(newDevice);

						InputtedInventoryNumber = "";
						InputtedNetworkName = "";
						InputtedDeviceDeviceAccountName = "";
						InputtedDevicePassword = "";
						MessageToUser = "Устройство добавлено";
					}
					catch (System.Exception)
					{
						MessageToUser = "Устройство с таким инвентарным номер уже существует";
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
					DeviceModel.Remove(DeviceModel.Find(SelectedDevice.InventoryNumber));
					DeviceModel.SaveChanges();
					AllDevices.Remove(SelectedDevice);
				},
				(obj) => SelectedDevice != null
			);

			AddDeviceAccountToDeviceCommand = new ButtonCommand(
				(obj) =>
				{

				},
				(obj) => SelectedDevice.DeviceType.Name != "Коммутатор"
			);

			RemoveDeviceAccountFromDeviceCommand = new ButtonCommand(
				(obj) =>
				{
					DeviceAccountModel.Remove(SelectedDeviceAccount);
					DeviceAccountModel.SaveChanges();

					SelectedDeviceDeviceAccounts.Remove(SelectedDeviceAccount);
				},
				(obj) => SelectedDeviceAccount != null
			);

			AddIPToDeviceCommand = new ButtonCommand(
				(obj) =>
				{

				}
			);

			RemoveIPFromDeviceCommand = new ButtonCommand(
				(obj) =>
				{
					IPAddressModel.Remove(SelectedIP);
					IPAddressModel.SaveChanges();

					SelectedDeviceIPAddresses.Remove(SelectedIP);
				},
				(obj) => SelectedIP != null
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

		public ObservableCollection<DeviceAccount> SelectedDeviceDeviceAccounts
		{
			get => _selectedDeviceDeviceAccounts;
			set
			{
				_selectedDeviceDeviceAccounts = value;
				OnPropertyChanged("SelectedDeviceDeviceAccounts");
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
				OnPropertyChanged("SelectedDevice");

				// Getting all device's accounts
				SelectedDeviceDeviceAccounts = DeviceAccountModel.All().
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
		}

		public Housing SelectedHousing
		{
			get => _selectedHousing;
			set
			{
				_selectedHousing = value;
				SelectedHousingCabinets = _allCabinets.
					Where(c => c.HousingID == _selectedHousing.ID).
					ToList();
				if (_selectedHousing != null)
					OnPropertyChanged("SelectedHousing");
			}
		}

		public int SelectedHousingIndex
		{
			get => _selectedHousingIndex;
			set
			{
				_selectedHousingIndex = value;
				OnPropertyChanged(nameof(SelectedHousingIndex));
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

		public IPAddress SelectedIP { get; set; }

		public DeviceType SelectedDeviceType { get; set; }

		public ButtonCommand AddDeviceCommand { get; }

		public ButtonCommand RemoveDeviceCommand { get; }

		public ButtonCommand OpenAddDeviceViewCommand { get; }

		public ButtonCommand AddIPToDeviceCommand { get; }

		public ButtonCommand RemoveIPFromDeviceCommand { get; }

		public ButtonCommand AddDeviceAccountToDeviceCommand { get; }

		public ButtonCommand RemoveDeviceAccountFromDeviceCommand { get; }

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

		public string InputtedDeviceDeviceAccountName
		{
			get => _inputtedDeviceDeviceAccountName;
			set
			{
				_inputtedDeviceDeviceAccountName = value;
				OnPropertyChanged("InputtedDeviceDeviceAccountName");
			}
		}

		public string InputtedDevicePassword
		{
			get => _inputtedDevicePassword;
			set
			{
				_inputtedDevicePassword = value;
				OnPropertyChanged("InputtedDevicePassword");
			}
		}
	}
}
