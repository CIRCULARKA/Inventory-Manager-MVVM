namespace InventoryManager.Models
{
	public class User
	{
		private string _lastName;

		private string _firstName;

		private string _middleName;

		private string _login;

		private string _password;

		private Group _userGroup;

		public string LastName { get; set; }


		public string FirstName { get; set; }


		public string MiddleName { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public int UserGroupID { get; set; }

		public Group UserGroup { get; set; }

	}
}
