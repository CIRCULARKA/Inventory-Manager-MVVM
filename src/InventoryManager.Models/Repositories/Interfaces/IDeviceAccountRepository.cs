using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface IDeviceAccountRepository : IRepository
	{
		void AddDeviceAccount(DeviceAccount newAcc) =>
			DataContext.DeviceAccounts.Add(newAcc);

		void RemoveDeviceAccount(DeviceAccount accToRemove) =>
			DataContext.DeviceAccounts.Remove(accToRemove);

		void UpdateDeviceAccount(DeviceAccount accToUpdate) =>
			DataContext.DeviceAccounts.Update(accToUpdate);

		IEnumerable<DeviceAccount> AllDeviceAccounts { get; }
	}
}
