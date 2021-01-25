namespace InventoryManager.Models
{
	public class Device : NotifyingModel
	{
		public string InventoryNumber { get; set; }

		public int DeviceTypeID { get; set; }

		public DeviceType DeviceType { get; set; }

		public string NetworkName { get; set; }
	}
}
