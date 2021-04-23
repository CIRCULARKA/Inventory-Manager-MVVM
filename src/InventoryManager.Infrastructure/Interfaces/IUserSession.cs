using InventoryManager.Models;

namespace InventoryManager.Infrastructure
{
	interface IUserSession
	{
		void AuthorizeUser(User user, UserAccessRules rules);

		bool IsAuthorizedUserAllowedTo(UserActions action);
	}
}
