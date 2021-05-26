using System.Collections.Generic;
using System;

namespace InventoryManager.UsersAccess
{
	/// <summary>
	/// Class for setting what user can do with he's
	/// rights in the system
	/// User has no rights by default
	/// </summary>
	public class UserAccessRules
	{
		private Dictionary<UserActions, bool> _actions;

		public UserAccessRules()
		{
			_actions = new Dictionary<UserActions, bool>();

			foreach (UserActions action in AllUserActions)
			_actions.Add(action, false);
		}

		private Array AllUserActions =>
			Enum.GetValues(typeof(UserActions));

		public void AllowAction(UserActions action) =>
			_actions[action] = true;

		public bool IsActionAllowed(UserActions action) =>
			_actions[action];

		public void AllowEverything()
		{
			foreach (UserActions action in AllUserActions)
				_actions[action] = true;
		}
	}
}
