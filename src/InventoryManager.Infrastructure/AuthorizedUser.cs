using InventoryManager.Models;

namespace InventoryManager.Infrastructure
{
	public class AuthorizedUser
	{
		public User User { get; set; }

		public UserAccessRights AccessLevel =>
			(UserAccessRights)User.UserGroupID;
	}
}
