using InventoryManager.Models;
using System;
using System.Collections.Generic;

namespace InventoryManager.Events
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

		public static event Action<IEnumerable<Device>> OnDeviceFilteringCriteriaChanged;

		public static void RaiseOnDeviceFilteringCriteriaChanged(IEnumerable<Device> filteredList) =>
			OnDeviceFilteringCriteriaChanged?.Invoke(filteredList);

		public static event Action<Housing> OnDeviceHosuingChanged;

		public static void RaiseOnDeviceHousingChanged(Housing housing) =>
			OnDeviceHosuingChanged?.Invoke(housing);

		public static event Action<string, byte> OnNetworkConfigurationChanged;

		public static void RaiseOnNetworkConfigurationChanged(string address, byte mask) =>
			OnNetworkConfigurationChanged?.Invoke(address, mask);

		public static event Action<Software> OnSoftwareAdded;

		public static void RaiseOnSoftwareAdded(Software software) =>
			OnSoftwareAdded?.Invoke(software);
	}
}
