using InventoryManager.Commands;
using InventoryManager.Models;
using InventoryManager.Data;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.ViewModels
{
	public class AddDeviceViewModel : ViewModelBase
	{
		private string _messageToUser;

		public AddDeviceViewModel()
		{
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

		public string InputtedInventoryNumber { get; set; }

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

		public string InputtedNetworkName { get; set; }
	}
}
