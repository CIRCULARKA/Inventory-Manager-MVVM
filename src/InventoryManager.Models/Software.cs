namespace InventoryManager.Models
{
	public class Software
	{
		public int ID { get; set; }

		public int DeviceID { get; set; }

		public int SoftwareTypeID { get; set; }

		public SoftwareType Type { get; set; }
	}
}
