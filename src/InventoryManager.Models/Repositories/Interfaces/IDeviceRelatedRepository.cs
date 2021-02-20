using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Models
{
	public interface IDeviceRelatedRepository
		: IDeviceRepository, IDeviceAccountRepository,
			IIPAddressRepository, IDeviceTypeRepository,
			IHousingRepository, ICabinetRepository, IDeviceMovementHistoryNoteRepository
	{
		IQueryable<DeviceAccount> GetAllDeviceAccounts(Device device) =>
			DataContext.DeviceAccounts.Where(a => a.DeviceID == device.ID);

		IQueryable<DeviceMovementHistoryNote> GetAllDeviceHistoryNotes(Device device) =>
			DataContext.
			DeviceMovementHistoryNotes.
			Include(h => h.TargetCabinet).
			Where(dmn => dmn.DeviceID == device.ID);

		IQueryable<IPAddress> GetAllDeviceIPAddresses(Device device) =>
			DataContext.IPAddresses.Where(ip => ip.DeviceID == device.ID);
	}
}
