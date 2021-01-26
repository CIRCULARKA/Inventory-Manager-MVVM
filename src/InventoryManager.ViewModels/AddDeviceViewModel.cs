using InventoryManager.Commands;
using InventoryManager.Models;
using InventoryManager.Data;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.ViewModels
{
	public class AddDeviceViewModel : ViewModelBase
	{
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
				}
			);
		}

		public IEnumerable<DeviceType> DeviceTypes =>
			DataContext.DeviceTypes.ToList();

		public ButtonCommand AddDeviceCommand { get; }

		public string InputtedInventoryNumber { get; set; }

		public DeviceType SelectedDeviceType { get; set; }

		public string InputtedNetworkName { get; set; }
	}
}
