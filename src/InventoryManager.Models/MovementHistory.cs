using System;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.Models
{
	public class DeviceMovementHistory : ModelBase<DeviceMovementHistory>
	{
		public int ID { get; set; }

		public int DeviceID { get; set; }

		public int TargetHousingID { get; set; }

		public Housing TargetHousing { get; set; }

		public int TargetCabinetID { get; set; }

		public Cabinet TargetCabinet { get; set; }

		public string Reason { get; set; }

		public DateTime Date { get; set; }

		public override List<DeviceMovementHistory> All() =>
			DataContext.DeviceMovementHistory.ToList();
	}
}
