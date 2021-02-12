using System.Collections.Generic;
using InventoryManager.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InventoryManager.Models
{
	public class Device : ModelBase<Device>
	{
		public int ID { get; set; }

		public string InventoryNumber { get; set; }

		public int DeviceTypeID { get; set; }

		public DeviceType DeviceType { get; set; }

		public string NetworkName { get; set; }

		public int CabinetID { get; set; }

		public Cabinet Cabinet { get; set; }

		public void AddDeviceAccount(Device device, DeviceAccount acc)
		{
			DataContext.DeviceAccounts.Add(acc);
			DataContext.Devices.Update(device);
		}

		public override void Remove(Device device)
		{
			DataContext.DeviceAccounts.RemoveRange(
				DataContext.
				DeviceAccounts.
				Where(a => a.DeviceID == device.ID)
			);
			DataContext.Devices.Remove(device);
		}

		public override List<Device> All() =>
			DataContext.
			Devices.
			Include(d => d.Cabinet).
			// Include(d => d.Housing).
			Include(c => c.DeviceType).ToList();
	}
}
