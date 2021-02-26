namespace InventoryManager.Models
{
	public interface IDeviceAccountRepository : IRepository
	{
		void AddDeviceAccount(DeviceAccount newAcc);

		void RemoveDeviceAccount(DeviceAccount accToRemove);

		void UpdateDeviceAccount(DeviceAccount accToUpdate);
	}
}
