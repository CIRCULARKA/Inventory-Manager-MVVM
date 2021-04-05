using System.Collections.Generic;
using System;

namespace InventoryManager.Infrastructure
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

			foreach (UserActions action in Enum.GetValues(typeof(UserActions)))
			_actions.Add(action, false);
		}

		public void AllowAction(UserActions action) =>
			_actions[action] = true;

		public bool IsActionAllowed(UserActions action) =>
			_actions[action];
	}
}
