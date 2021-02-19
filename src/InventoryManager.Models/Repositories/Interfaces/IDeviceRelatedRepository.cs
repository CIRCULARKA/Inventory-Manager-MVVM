namespace InventoryManager.Models
{
	public interface IDeviceRelatedRepository
		: IDeviceRepository, IDeviceAccountRepository, IIPAddressRepository, IDeviceTypeRepository
	{
	}
}
