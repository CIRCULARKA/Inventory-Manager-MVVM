using InventoryManager.Models;

namespace InventoryManager.Infrastructure
{
	public static class UserSession
	{
		private static UserAccessRules _rules;

		private static User _authorizedUser;

		public static void AuthorizeUser(User user, UserAccessRules rules)
		{
			_authorizedUser = user;
			_rules = rules;
		}

		public static User AuthorizedUser =>
			_authorizedUser;

		public static UserAccessRights AuthorizedUserAccessLevel =>
			(UserAccessRights)_authorizedUser.UserGroupID;

		public static UserAccessRights GetUserAccessLevel(User user) =>
			(UserAccessRights)_authorizedUser.UserGroupID;

		public static bool IsAuthorizedUserAllowedTo(UserActions action) =>
			_rules.IsActionAllowed(action);
	}
}
