using System;

namespace InventoryManager.Infrastructure
{
	public static class UIEvents
	{
		public static event Action OnShowAddIPAddressViewCommandExecuted;

		public static void RaiseOnShowAddIPAddressViewCommandExecuted() =>
			OnShowAddIPAddressViewCommandExecuted?.Invoke();
	}
}
