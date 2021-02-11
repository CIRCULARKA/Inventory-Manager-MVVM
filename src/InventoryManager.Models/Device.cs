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

		public int LocationID { get; set; }

		public Location Location { get; set; }

		public void AddAccount(Device device, Account acc)
		{
			DataContext.Accounts.Add(acc);
			DataContext.Devices.Update(device);
		}

		public override void Remove(Device device)
		{
			DataContext.Accounts.RemoveRange(
				DataContext.
				Accounts.
				Where(a => a.DeviceID == device.ID)
			);
			DataContext.Devices.Remove(device);
		}

		public override List<Device> All() =>
			DataContext.
			Devices.
			Include(c => c.DeviceType).ToList();
	}
}
