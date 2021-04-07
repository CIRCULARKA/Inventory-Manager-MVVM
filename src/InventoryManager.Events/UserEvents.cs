using InventoryManager.Models;
using System;

namespace InventoryManager.Events
{
	public static class UserEvents
	{
		public static event Action<User> OnUserAdded;

		public static void RaiseOnUserAdded(User user) =>
			OnUserAdded?.Invoke(user);
	}
}
