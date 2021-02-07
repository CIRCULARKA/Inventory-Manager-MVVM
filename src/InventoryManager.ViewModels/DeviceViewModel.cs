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

		private readonly Device _deviceModel;

		private Device _selectedDevice;

		private readonly DeviceType _deviceTypeModel;

		private readonly IPAddress _ipAddressModel;

		private readonly Account _accountModel;

		private ObservableCollection<Device> _devices;

		private ObservableCollection<Account> _allAccounts;

		private ObservableCollection<Account> _selectedDeviceAccounts;

		private ObservableCollection<IPAddress> _allIPAddresses;

		private ObservableCollection<IPAddress> _selectedDeviceIPAddresses;

		public DeviceViewModel()
		{
			_deviceModel = new Device();
			_deviceTypeModel = new DeviceType();
			_ipAddressModel = new IPAddress();
			_accountModel = new Account();

			_devices = _deviceModel.All().ToObservableCollection();
			_allIPAddresses = _ipAddressModel.All().ToObservableCollection();
			_allAccounts = _accountModel.All().ToObservableCollection();

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
						_deviceModel.Add(newDevice);
						_deviceModel.SaveChanges();

						// Add DeviceType explicitly in order to avoid db exception
						newDevice.DeviceType = SelectedDeviceType;
						DevicesToShow.Add(newDevice);

						InputtedInventoryNumber = "";
						InputtedNetworkName = "";
						InputtedDeviceAccountName = "";
						InputtedDevicePassword = "";
						MessageToUser = "Устройство добавлено";
					}
					catch (System.Exception e)
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
					_deviceModel.Remove(_deviceModel.Find(SelectedDevice.InventoryNumber));
					_deviceModel.SaveChanges();
					DevicesToShow.Remove(SelectedDevice);
				},
				(obj) => SelectedDevice != null
			);

			RemoveAccountFromDeviceCommand = new ButtonCommand(
				(obj) =>
				{
					_accountModel.Remove(SelectedAccount);
					_accountModel.SaveChanges();

					SelectedDeviceAccounts.Remove(SelectedAccount);
				},
				(obj) => SelectedAccount != null
			);

			RemoveIPFromDeviceCommand = new ButtonCommand(
				(obj) =>
				{
					_ipAddressModel.Remove(SelectedIP);
					_ipAddressModel.SaveChanges();

					SelectedDeviceIPAddresses.Remove(SelectedIP);
				},
				(obj) => SelectedIP != null
			);
		}

		public IEnumerable<DeviceType> DeviceTypes =>
			_deviceTypeModel.All();

		public ButtonCommand AddDeviceCommand { get; }

		public ButtonCommand RemoveDeviceCommand { get; }

		public ButtonCommand OpenAddDeviceViewCommand { get; }

		public ButtonCommand AddIPToDeviceCommand { get; }

		public ButtonCommand RemoveIPFromDeviceCommand { get; }

		public ButtonCommand AddAccountToDeviceCommand { get; }

		public ButtonCommand RemoveAccountFromDeviceCommand { get; }

		public ObservableCollection<Device> DevicesToShow =>
			_devices;

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


		public Device SelectedDevice
		{
			get => _selectedDevice;
			set
			{
				_selectedDevice = value;

				// Getting all device's accounts
				SelectedDeviceAccounts = _allAccounts.
					Where(a => a.DeviceID == SelectedDevice.ID).
					ToObservableCollection();

				// Getting all device's IP's
				SelectedDeviceIPAddresses = _allIPAddresses.
					Where(ip => ip.DeviceID == SelectedDevice.ID).
					ToObservableCollection();

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
