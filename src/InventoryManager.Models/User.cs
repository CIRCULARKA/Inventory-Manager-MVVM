using System;

namespace InventoryManager.Models
{
	public class User
	{
		public Guid ID { get; set; }

		public string LastName { get; set; }

		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public Guid UserGroupID { get; set; }

		public UserGroup UserGroup { get; set; }

		public string FullName => $"{LastName} {FirstName} {MiddleName}";
	}
}
