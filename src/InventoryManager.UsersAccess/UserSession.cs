using InventoryManager.Models;
using System;

namespace InventoryManager.UsersAccess
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
			DetermineAccessRights(_authorizedUser);

		public UserAccessRights GetAccessLevel(User user)
		{
			try { return DetermineAccessRights(user); }
			catch { throw new Exception($"User can't be null"); }
		}

		public bool IsAuthorizedUserAllowedTo(UserActions action)
		{
			try { return _rules.IsActionAllowed(action); }
			catch (NullReferenceException)
			{ return false; }
		}

		private UserAccessRights DetermineAccessRights(User user)
		{
			if (user.UserGroup.Name == "Техник")
				return UserAccessRights.Technician;
			if (user.UserGroup.Name == "Администратор")
				return UserAccessRights.Administrator;
			else return UserAccessRights.Superuser;
		}
	}
}
