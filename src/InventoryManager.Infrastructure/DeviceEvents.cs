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

		public static event Action<IPAddress> OnDeviceIPAdded;

		public static void RaiseOnDeviceIPAdded(IPAddress ip) =>
			OnDeviceIPAdded?.Invoke(ip);

		public static event Action<IPAddress> OnDeviceIPRemoved;

		public static void RaiseOnDeviceIPRemoved(IPAddress ip) =>
			OnDeviceIPRemoved?.Invoke(ip);
	}
}
