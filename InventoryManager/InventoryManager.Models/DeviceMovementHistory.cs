using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Models
{
	public class DeviceMovementHistory : ModelBase<DeviceMovementHistory>
	{
		public int ID { get; set; }

		public int DeviceID { get; set; }

		public Device Device { get; set; }

		public int TargetCabinetID { get; set; }

		public Cabinet TargetCabinet { get; set; }

		public string Reason { get; set; }

		public DateTime Date { get; set; }

		public override List<DeviceMovementHistory> All() =>
			DataContext.DeviceMovementHistory.ToList();

		public List<DeviceMovementHistory> All(Device device) =>
			DataContext.
			DeviceMovementHistory.
			Include(dmh => dmh.Device).
			Include(dmh => dmh.TargetCabinet).
			ThenInclude(cabinet => cabinet.Housing).
			Where(dh => dh.DeviceID == device.ID).ToList();
	}
}
