namespace InventoryManager.Models
{
	public class Device : ModelBase<Device>
	{
		public string InventoryNumber { get; set; }

		public int DeviceConfigurationID { get; set; }

		public DeviceConfiguration DeviceConfiguration { get; set; }

		public int DeviceTypeID { get; set; }

		public DeviceType DeviceType { get; set; }

		public string NetworkName { get; set; }
	}
}
