using System.Linq;

namespace InventoryManager.Models
{
	public interface IDeviceRelatedRepository
		: IDeviceRepository, IDeviceAccountRepository, IIPAddressRepository, IDeviceTypeRepository
	{
		IQueryable<DeviceAccount> AllDeviceAccounts(Device device) =>
			DataContext.DeviceAccounts.Where(a => a.DeviceID == device.ID);
	}
}
