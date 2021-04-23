using InventoryManager.Models;

namespace InventoryManager.Infrastructure
{
	interface IUserSession
	{
		void AuthorizeUser(User user);

		bool IsAuthorizedUserAllowedTo(UserActions action);
	}
}
