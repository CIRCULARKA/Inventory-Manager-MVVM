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
		private string _messageToUser;

		private string _inputtedInventoryNumber;

		private string _inputtedNetworkName;

		private ObservableCollection<Device> _devices;

		public DeviceViewModel()
		{
			_devices = DataContext.Devices.Include(d => d.DeviceType).ToList().ToObservableCollection();

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
					Devices.Add(
						new Device
						{
							InventoryNumber = InputtedInventoryNumber,
							DeviceType = SelectedDeviceType,
							NetworkName = InputtedNetworkName
						}
					);

					DataContext.Devices.Add(
						new Device
						{
							InventoryNumber = InputtedInventoryNumber,
							DeviceType = SelectedDeviceType,
							NetworkName = InputtedNetworkName
						}
					);
					DataContext.SaveChanges();

					InputtedInventoryNumber = "";
					InputtedNetworkName = "";
					MessageToUser = "Устройство добавлено";
				},
				(obj) =>
				{
					return !string.IsNullOrEmpty(InputtedInventoryNumber) &&
						!string.IsNullOrEmpty(InputtedNetworkName);
				}
			);

			RemoveDeviceCommand = new ButtonCommand(
				(obj) =>
				{
					DataContext.Devices.Remove(SelectedDevice);
					Devices.Remove(SelectedDevice);
					DataContext.SaveChanges();
				},
				(obj) => Devices.Count > 0 && SelectedDevice != null
			);
		}

		public IEnumerable<DeviceType> DeviceTypes =>
			DataContext.DeviceTypes.ToList();

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

		public string MessageToUser
		{
			get => _messageToUser;
			set
			{
				_messageToUser = value;
				OnPropertyChanged("MessageToUser");
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
	}
}
