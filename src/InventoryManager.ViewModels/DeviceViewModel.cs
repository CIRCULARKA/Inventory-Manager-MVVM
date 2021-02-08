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

		private string _inputtedDeviceAccountName;

		private string _inputtedDevicePassword;

		private Device _selectedDevice;

		private Housing _selectedHousing;

		private ObservableCollection<Account> _selectedDeviceAccounts;

		private ObservableCollection<IPAddress> _selectedDeviceIPAddresses;

		public DeviceViewModel()
		{
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
						DevicesToShow.Add(newDevice);

						InputtedInventoryNumber = "";
						InputtedNetworkName = "";
						InputtedDeviceAccountName = "";
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
					DevicesToShow.Remove(SelectedDevice);
				},
				(obj) => SelectedDevice != null
			);

			RemoveAccountFromDeviceCommand = new ButtonCommand(
				(obj) =>
				{
					AccountModel.Remove(SelectedAccount);
					AccountModel.SaveChanges();

					SelectedDeviceAccounts.Remove(SelectedAccount);
				},
				(obj) => SelectedAccount != null
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

		public IEnumerable<DeviceType> AllDeviceTypes =>
			DeviceTypeModel.All();

		public List<Housing> AllHousings =>
			HousingModel.All();

		public List<Cabinet> SelectedHousingCabinets { get; set; }

		public Cabinet SelectedCabinet { get; set; }

		public Housing SelectedHousing
		{
			get => _selectedHousing;
			set
			{
				_selectedHousing = value;
				SelectedHousingCabinets = CabinetModel.All(_selectedHousing);
				OnPropertyChanged("SelectedHousing");
			}
		}

		public ButtonCommand AddDeviceCommand { get; }

		public ButtonCommand RemoveDeviceCommand { get; }

		public ButtonCommand OpenAddDeviceViewCommand { get; }

		public ButtonCommand AddIPToDeviceCommand { get; }

		public ButtonCommand RemoveIPFromDeviceCommand { get; }

		public ButtonCommand AddAccountToDeviceCommand { get; }

		public ButtonCommand RemoveAccountFromDeviceCommand { get; }

		public ObservableCollection<Device> DevicesToShow =>
			DeviceModel.All().ToObservableCollection();

		public ObservableCollection<IPAddress> SelectedDeviceIPAddresses
		{
			get => _selectedDeviceIPAddresses;
			set
			{
				_selectedDeviceIPAddresses = value;
				OnPropertyChanged("SelectedDeviceIPAddresses");
			}
		}

		public ObservableCollection<Account> SelectedDeviceAccounts
		{
			get => _selectedDeviceAccounts;
			set
			{
				_selectedDeviceAccounts = value;
				OnPropertyChanged("SelectedDeviceAccounts");
			}
		}

		public Housing SelectedDeviceHousing { get; set; }

		public List<Housing> HousingsToShow => HousingModel.All();

		public List<Cabinet> CabinetsToShow { get; set; }

		public Device SelectedDevice
		{
			get => _selectedDevice;
			set
			{
				_selectedDevice = value;

				// Getting all device's accounts
				SelectedDeviceAccounts = AccountModel.All().
					Where(a => a.DeviceID == SelectedDevice.ID).
					ToObservableCollection();

				// Getting all device's IP's
				SelectedDeviceIPAddresses = IPAddressModel.All().
					Where(ip => ip.DeviceID == SelectedDevice.ID).
					ToObservableCollection();

				// Getting device's housing
				SelectedHousing = SelectedDevice.Cabinet.Housing;

				// Getting housing's cabinets
				CabinetsToShow = CabinetModel.All(SelectedHousing);

				// Select device's cabinet
				SelectedCabinet = SelectedDevice.Cabinet;

				OnPropertyChanged("SelectedDevice");
			}
		}

		public Account SelectedAccount { get; set; }

		public IPAddress SelectedIP { get; set; }

		public DeviceType SelectedDeviceType { get; set; }

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

		public string InputtedDeviceAccountName
		{
			get => _inputtedDeviceAccountName;
			set
			{
				_inputtedDeviceAccountName = value;
				OnPropertyChanged("InputtedDeviceAccountName");
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
