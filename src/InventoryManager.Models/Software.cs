using System;

namespace InventoryManager.Models
{
	public class Software
	{
		public Guid ID { get; set; }

		public Guid DeviceID { get; set; }

		public Device Device { get; set; }

		public Guid TypeID { get; set; }

		public SoftwareType Type { get; set; }

		public Guid ConfigurationID { get; set; }

		public SoftwareConfiguration Configuration { get; set; }
	}
}
