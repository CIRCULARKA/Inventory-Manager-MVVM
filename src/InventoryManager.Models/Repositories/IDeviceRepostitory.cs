using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface IDeviceRepostiroy
	{
		void AddDevice(Device newDevice);

		void RemoveDevice(Device deviceToDelete);

		void UpdateDevice(Device deviceToUpdate);

		IEnumerable<Device> AllDevices { get; }
	}
}
