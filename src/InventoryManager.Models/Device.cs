using System;

namespace InventoryManager.Models
{
	public class Device
	{
		public Guid ID { get; set; }

		public string InventoryNumber { get; set; }

		public Guid DeviceTypeID { get; set; }

		public DeviceType DeviceType { get; set; }

		public string NetworkName { get; set; }

		public Guid? CabinetID { get; set; }

		public Cabinet Cabinet { get; set; }
	}
}
