using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface IDeviceRepostiroy
	{
		void AddDevice(Device device);

		void RemoveDevice(Device device);

		void UpdateDevice(Device device);

		IEnumerable<Device> AllDevices { get; }
	}
}
