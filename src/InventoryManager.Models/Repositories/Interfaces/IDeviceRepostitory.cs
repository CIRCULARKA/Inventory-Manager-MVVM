using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface IDeviceRepository : IRepository
	{
		void AddDevice(Device newDevice);

		void RemoveDevice(Device deviceToDelete);

		void UpdateDevice(Device deviceToUpdate);

		void FindDevice(params object[] keys);

		IEnumerable<Device> AllDevices { get; }
	}
}
