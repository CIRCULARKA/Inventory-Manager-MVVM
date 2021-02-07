using System.Collections.Generic;
using InventoryManager.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InventoryManager.Models
{
	public class Device : ModelBase<Device>
	{
		private List<Device> _allDevices;

		public Device() =>
			_allDevices = DataContext.Devices.Include(d => d.DeviceType).ToList();

		public int ID { get; set; }

		public string InventoryNumber { get; set; }

		public int DeviceTypeID { get; set; }

		public DeviceType DeviceType { get; set; }

		public string NetworkName { get; set; }

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

		public override List<Device> All() => _allDevices;
	}
}
