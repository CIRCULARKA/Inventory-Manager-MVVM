namespace InventoryManager.Models
{
	public class DeviceAccount
	{
		public int ID { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public int DeviceID { get; set; }

		public Device Device { get; set; }
	}
}
