using System;

namespace InventoryManager.Models
{
	public class IPAddress
	{
		public Guid ID { get; set; }

		public string Address { get; set; }

		public Guid? DeviceID { get; set; }

		public Device Device { get; set; }

		public bool IsAvailable => DeviceID != Guid.Empty;
	}
}
