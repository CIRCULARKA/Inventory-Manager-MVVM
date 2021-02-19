using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public interface IDeviceTypeRepository : IRepository
	{
		void AddDeviceType(DeviceType newType) =>
			DataContext.DeviceTypes.Add(newType);

		void RemoveDeviceType(DeviceType typeToRemove) =>
			DataContext.DeviceTypes.Remove(typeToRemove);


		void UpdateDeviceType(DeviceType typeToUpdate) =>
			DataContext.DeviceTypes.Update(typeToUpdate);

		IEnumerable<DeviceType> AllDeviceTypes =>
			DataContext.DeviceTypes.ToList();
	}

}
