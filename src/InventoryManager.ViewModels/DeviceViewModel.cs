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

		public DeviceViewModel()
		{
			OpenAddDeviceViewCommand = new ButtonCommand(
				(obj) =>
				{
					var addDeviceWindow = new AddDeviceView();
					addDeviceWindow.Show();
				}
			);

			AddDeviceCommand = new ButtonCommand(
				(obj) =>
				{
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
		}

		public IEnumerable<DeviceType> DeviceTypes =>
			DataContext.DeviceTypes.ToList();

		public ButtonCommand AddDeviceCommand { get; }

		public ButtonCommand OpenAddDeviceViewCommand { get; }

		public ObservableCollection<Device> Devices =>
			DataContext.Devices.Include(d => d.DeviceType).ToList().ToObservableCollection();

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
