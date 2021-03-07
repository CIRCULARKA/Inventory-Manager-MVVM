using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface IIPAddressRepository : IRepository
	{
		void SetNewRangeOfIPAddresses(IEnumerable<IPAddress> range);

		IEnumerable<IPAddress> AllIPAddresses { get; }
	}
}
