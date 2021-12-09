using System;

namespace InventoryManager.Models
{
	public class DeviceMovementHistoryNote
	{
		public Guid ID { get; set; }

		public Guid DeviceID { get; set; }

		public Guid? TargetCabinetID { get; set; }

		public Cabinet TargetCabinet { get; set; }

		public string Reason { get; set; }

		public DateTime Date { get; set; }
	}
}
