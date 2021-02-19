using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InventoryManager.Models
{
	public interface IDeviceRepository : IRepository
	{
		void AddDevice(Device newDevice) =>
			DataContext.Devices.Add(newDevice);

		void RemoveDevice(Device deviceToDelete) =>
			DataContext.Devices.Remove(deviceToDelete);

		void UpdateDevice(Device deviceToUpdate) =>
			DataContext.Devices.Update(deviceToUpdate);

		IEnumerable<Device> AllDevices =>
			DataContext.
			Devices.
			Include(d => d.Cabinet).
				ThenInclude(c => c.Housing).
			Include(d => d.DeviceType).
			ToList();
	}
}
