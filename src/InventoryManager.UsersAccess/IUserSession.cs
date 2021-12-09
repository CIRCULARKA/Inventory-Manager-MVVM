using InventoryManager.Models;

namespace InventoryManager.UsersAccess
{
	public interface IUserSession
	{
		User AuthorizedUser { get; }

		void AuthorizeUser(User user, UserAccessRules rules);

		bool IsAuthorizedUserAllowedTo(UserActions action);

		UserAccessRights GetAccessLevel(User user);
	}
}
