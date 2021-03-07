using System.Linq;

namespace InventoryManager.Models
{
	public interface IDeviceRelatedRepository
		: IDeviceRepository, IDeviceAccountRepository,
			IIPAddressRepository, IDeviceTypeRepository,
			IHousingRepository, ICabinetRepository, IDeviceMovementHistoryNoteRepository
	{
		IQueryable<DeviceAccount> GetAllDeviceAccounts(Device device);

		IQueryable<DeviceMovementHistoryNote> GetAllDeviceHistoryNotes(Device device);

		IQueryable<IPAddress> GetAllDeviceIPAddresses(Device device);

		void AddIPToDevice(IPAddress ip, Device device);

		void RemoveIPFromDevice(IPAddress ip, Device device);

		void DeleteAllDeviceMovementHistory(Device device);
	}
}
