using System;

namespace InventoryManager.Models
{
	public class DeviceMovementHistoryNote
	{
		public int ID { get; set; }

		public int DeviceID { get; set; }

		public int TargetCabinetID { get; set; }

		public Cabinet TargetCabinet { get; set; }

		public string Reason { get; set; }

		public DateTime Date { get; set; }
	}
}
