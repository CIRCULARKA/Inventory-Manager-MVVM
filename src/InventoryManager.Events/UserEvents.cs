using InventoryManager.Models;
using System;

namespace InventoryManager.Events
{
	public static class UserEvents
	{
		public static event Action<User> OnUserAdded;

		public static void RaiseOnUserAdded(User user) =>
			OnUserAdded?.Invoke(user);

		public static event Action<User> OnUserLoggedIn;

		public static void RaiseOnUserLoggedIn(User user) =>
			OnUserLoggedIn?.Invoke(user);

		public static event Action OnUserLoggedOut;

		public static void RaiseOnUserLoggedOut() =>
			OnUserLoggedOut?.Invoke();
	}
}
