using InventoryManager.Models;

namespace InventoryManager.Infrastructure
{
	public class AuthorizedUser
	{
		private UserAccessRules _rules;

		private User _authorizedUser;

		public AuthorizedUser(User user, UserAccessRules rules)
		{
			_authorizedUser = user;
			_rules = rules;
		}

		public User User { get; set; }

		public static UserAccessRights GetUserAccessLevel(User user) =>
			(UserAccessRights)user.UserGroupID;

		public bool IsAllowedTo(UserActions action) =>
			_rules.IsActionAllowed(action);
	}
}
