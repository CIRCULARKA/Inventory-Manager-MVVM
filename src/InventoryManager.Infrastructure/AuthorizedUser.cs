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

		public UserAccessRights AccessLevel =>
			(UserAccessRights)User.UserGroupID;

		public bool IsAllowedTo(UserActions action) =>
			_rules.IsActionAllowed(action);
	}
}
