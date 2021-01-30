namespace InventoryManager.Models
{
	public class User
	{
		public string LastName { get; set; }

		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public int UserGroupID { get; set; }

		public Group UserGroup { get; set; }
	}
}
