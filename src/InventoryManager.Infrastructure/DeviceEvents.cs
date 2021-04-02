using System;
using InventoryManager.Models;

namespace InventoryManager.Infrastructure
{
	public static class DeviceEvents
	{
		public static event Action<Device> OnDeviceSelectionChanged;

		public static void RaiseOnDeviceSelectionChanged(Device device) =>
			OnDeviceSelectionChanged?.Invoke(device);
	}
}
