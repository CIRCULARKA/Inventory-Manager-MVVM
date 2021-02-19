using System.Linq;

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
			DataContext.DeviceMovementHistory.Where(dmn => dmn.DeviceID == device.ID);
	}
}
