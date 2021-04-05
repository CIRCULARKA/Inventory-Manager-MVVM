using InventoryManager.Models;

namespace InventoryManager.Infrastructure
{
	public static class AuthorizedUser
	{
		public static User User { get; set; }

		public static UserAccessRights AccessLevel =>
			(UserAccessRights)User.UserGroupID;
	}
}