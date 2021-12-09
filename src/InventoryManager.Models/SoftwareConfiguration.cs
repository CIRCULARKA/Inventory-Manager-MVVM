using System;

namespace InventoryManager.Models
{
	public class SoftwareConfiguration
	{
		public Guid ID { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public string AdditionalInformation { get; set; }
	}
}
