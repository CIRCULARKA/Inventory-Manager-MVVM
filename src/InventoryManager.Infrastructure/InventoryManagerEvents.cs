using System;

namespace InventoryManager.Infrastructure
{
	public class InventoryManagerEvents
	{
		public static event Action OnNetworkMaskChanged;

		public static void RaiseOnNetworkMaskChangedEvent() =>
			OnNetworkMaskChanged?.Invoke();
	}
}
