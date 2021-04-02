using System;
using InventoryManager.Models;

namespace InventoryManager.Infrastructure
{
	public static class DeviceEvents
	{
		public static event Action<Device> OnDeviceSelectionChanged;

		public static void RaiseOnDeviceSelectionChanged(Device device) =>
			OnDeviceSelectionChanged?.Invoke(device);

		public static event Action<Device> OnNewDeviceAdded;

		public static void RaiseOnNewDeviceAdded(Device device) =>
			OnNewDeviceAdded?.Invoke(device);

		public static event Action<DeviceAccount> OnDeviceAccountAdded;

		public static void RaiseOnDeviceAccountAdded(DeviceAccount acc) =>
			OnDeviceAccountAdded?.Invoke(acc);
	}
}
