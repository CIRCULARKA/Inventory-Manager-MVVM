using Microsoft.EntityFrameworkCore;
using InventoryManager.Commands;
using InventoryManager.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using InventoryManager.Views;
using InventoryManager.Extensions;
using System.Linq;

namespace InventoryManager.ViewModels
{
	public class DeviceViewModel : ViewModelBase
	{
		private readonly Device _deviceModel;

		private readonly DeviceConfiguration _deviceConfigurationModel;

		private readonly DeviceType _deviceTypeModel;

		private string _messageToUser;

		private string _inputtedInventoryNumber;

		private string _inputtedNetworkName;

		private string _inputtedDeviceAccountName;

		private string _inputtedDevicePassword;

		private ObservableCollection<Device> _devices;

		public DeviceViewModel()
		{
			_deviceModel = new Device();
			_deviceConfigurationModel = new DeviceConfiguration();
			_deviceTypeModel = new DeviceType();

			_devices = _deviceModel.All().ToObservableCollection();

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
					// Firstly - create new row in DeviceConfiguration for the device
					// in order to avoid FK constraint failure
					var newDeviceConfiguration = new DeviceConfiguration
					{
						AccountName = InputtedDeviceAccountName,
						AccountPassword = InputtedDevicePassword
					};
					_deviceConfigurationModel.Add(newDeviceConfiguration);

					var newDevice = new Device
					{
						InventoryNumber = InputtedInventoryNumber,
						DeviceType = SelectedDeviceType,
						NetworkName = InputtedNetworkName,
						DeviceConfiguration = newDeviceConfiguration
					};

					Devices.Add(newDevice);

					_deviceModel.Add(newDevice);
					_deviceModel.SaveChanges();

					InputtedInventoryNumber = "";
					InputtedNetworkName = "";
					InputtedDeviceAccountName = "";
					InputtedDevicePassword = "";
					MessageToUser = "Устройство добавлено";
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
					Devices.Remove(SelectedDevice);
					_deviceModel.SaveChanges();
				},
				(obj) => Devices.Count > 0 && SelectedDevice != null
			);
		}

		public IEnumerable<DeviceType> DeviceTypes =>
			_deviceTypeModel.All();

		public ButtonCommand AddDeviceCommand { get; }

		public ButtonCommand RemoveDeviceCommand { get; }

		public ButtonCommand OpenAddDeviceViewCommand { get; }

		public ObservableCollection<Device> Devices =>
			_devices;

		public Device SelectedDevice { get; set; }

		public string InputtedInventoryNumber
		{
			get => _inputtedInventoryNumber;
			set
			{
				_inputtedInventoryNumber = value;
				OnPropertyChanged("InputtedInventoryNumber");
			}
		}

		public DeviceType SelectedDeviceType { get; set; }

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
