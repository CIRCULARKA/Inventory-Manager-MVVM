using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface IDeviceTypeRepository
	{
		void AddDeviceType(DeviceType newType);

		void RemoveDeviceType(DeviceType typeToRemove);

		void UpdateDeviceType(DeviceType typeToUpdate);

		IEnumerable<DeviceType> AllDeviceTypes { get; }
	}

}
