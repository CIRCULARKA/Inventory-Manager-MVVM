namespace InventoryManager.Models
{
	public class IPAddress
	{
		public int ID { get; set; }

		public string Address { get; set; }

		public int? DeviceID { get; set; }

		public Device Device { get; set; }

		public bool IsAvailable => DeviceID <= 0;
	}
}
