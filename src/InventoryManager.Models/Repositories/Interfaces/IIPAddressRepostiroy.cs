using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface IIPAddressRepository : IRepository
	{
		void AddIPAddress(IPAddress newIP);

		void RemoveIPAddress(IPAddress IPtoRemove);

		void UpdateIPAddress(IPAddress IPtoUpdate);

		IEnumerable<IPAddress> AllIPAddresses { get; }
	}
}
