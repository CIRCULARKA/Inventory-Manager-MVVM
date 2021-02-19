using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface IDeviceAccountRepository
	{
		void AddDeviceAccount(DeviceAccount newAcc);

		void RemoveDeviceAccount(DeviceAccount accToRemove);

		void UpdateDeviceAccount(DeviceAccount accToUpdate);

		IEnumerable<DeviceAccount> AllDeviceAccounts { get; }
	}
}
