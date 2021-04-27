namespace InventoryManager.Models
{
	public class SoftwareConfiguration
	{
		public int ID { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public string AdditionalInformation { get; set; }

		public int SoftwareID { get; set; }

		public Software Software { get; set; }
	}
}
