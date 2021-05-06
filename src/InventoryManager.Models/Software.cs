namespace InventoryManager.Models
{
	public class Software
	{
		public int ID { get; set; }

		public int DeviceID { get; set; }

		public Device Device { get; set; }

		public int TypeID { get; set; }

		public SoftwareType Type { get; set; }

		public int ConfigurationID { get; set; }

		public SoftwareConfiguration Configuration { get; set; }
	}
}
