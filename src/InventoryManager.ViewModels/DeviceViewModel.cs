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

					var newDevice = new Device
					{
						InventoryNumber = InputtedInventoryNumber,
						DeviceTypeID = SelectedDeviceType.ID,
						NetworkName = InputtedNetworkName,
						DeviceConfiguration = newDeviceConfiguration
					};

					try
					{
						_deviceModel.Add(newDevice);
						_deviceConfigurationModel.Add(newDeviceConfiguration);
						_deviceModel.SaveChanges();

						// Load DeviceType object after adding to db in order to avoid exception
						// and display name of device type in observable collection
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
					_deviceModel.Remove(_deviceModel.Find(SelectedDevice.InventoryNumber));
					_deviceModel.SaveChanges();
					DevicesToShow.Remove(SelectedDevice);
				},
				(obj) => SelectedDevice != null
			);
		}

		public IEnumerable<DeviceType> DeviceTypes =>
			_deviceTypeModel.All();

		public ButtonCommand AddDeviceCommand { get; }

		public ButtonCommand RemoveDeviceCommand { get; }

		public ButtonCommand OpenAddDeviceViewCommand { get; }

		public ObservableCollection<Device> DevicesToShow =>
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
