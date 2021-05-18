using InventoryManager.Models;

namespace InventoryManager.UsersAccess
{
	public interface IUserSession
	{
		void AuthorizeUser(User user, UserAccessRules rules);

		bool IsAuthorizedUserAllowedTo(UserActions action);

		UserAccessRights GetAccessLevel(User user);
	}
}
