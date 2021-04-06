using InventoryManager.Models;
using System;

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

		public static UserAccessRights GetUserAccessLevel(User user)
		{
			try { return (UserAccessRights)_authorizedUser.UserGroupID; }
			catch { throw new Exception($"No user is authorized. Use \"{nameof(AuthorizeUser)}\" firstly"); }
		}


		public static bool IsAuthorizedUserAllowedTo(UserActions action)
		{
			try { return _rules.IsActionAllowed(action); }
			catch (NullReferenceException)
			{ throw new Exception($"No user is authorized. Use \"{nameof(AuthorizeUser)}\" firstly"); }
		}
	}
}
