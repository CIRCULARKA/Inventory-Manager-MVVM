using InventoryManager.Models;
using System;

namespace InventoryManager.Infrastructure
{
	public class UserSession : IUserSession
	{
		private UserAccessRules _rules;

		private User _authorizedUser;

		public void AuthorizeUser(User user, UserAccessRules rules)
		{
			_authorizedUser = user;
			_rules = rules;
		}

		public User AuthorizedUser =>
			_authorizedUser;

		public UserAccessRights AuthorizedUserAccessLevel =>
			(UserAccessRights)_authorizedUser.UserGroupID;

		public UserAccessRights GetAccessLevel(User user)
		{
			try { return (UserAccessRights)user.UserGroupID; }
			catch { throw new Exception($"User can't be null"); }
		}

		public bool IsAuthorizedUserAllowedTo(UserActions action)
		{
			try { return _rules.IsActionAllowed(action); }
			catch (NullReferenceException)
			{ return false; }
		}
	}
}
