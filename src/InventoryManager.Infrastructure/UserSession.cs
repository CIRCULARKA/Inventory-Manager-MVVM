using InventoryManager.Models;
using InventoryManager.Events;
using System;

namespace InventoryManager.Infrastructure
{
	public class UserSession : IUserSession
	{
		private static UserAccessRules _rules;

		private static User _authorizedUser;

		public UserSession()
		{
			UserEvents.OnUserLoggedIn += (user) =>
			{
				UserSession.AuthorizeUser(
					user,
					UserRightsBuilder.GetUserRights(
						UserSession.GetAccessLevel(user)
					)
				);
			};
		}

		public static void AuthorizeUser(User user, UserAccessRules rules)
		{
			_authorizedUser = user;
			_rules = rules;
		}

		public static User AuthorizedUser =>
			_authorizedUser;

		public static UserAccessRights AuthorizedUserAccessLevel =>
			(UserAccessRights)_authorizedUser.UserGroupID;

		public static UserAccessRights GetAccessLevel(User user)
		{
			try { return (UserAccessRights)user.UserGroupID; }
			catch { throw new Exception($"User can't be null"); }
		}

		public static bool IsAuthorizedUserAllowedTo(UserActions action)
		{
			try { return _rules.IsActionAllowed(action); }
			catch (NullReferenceException)
			{ return false; }
		}
	}
}
