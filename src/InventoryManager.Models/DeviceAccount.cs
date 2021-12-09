using System;

namespace InventoryManager.Models
{
	public class DeviceAccount
	{
		public Guid ID { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public Guid DeviceID { get; set; }

		public Device Device { get; set; }
	}
}
