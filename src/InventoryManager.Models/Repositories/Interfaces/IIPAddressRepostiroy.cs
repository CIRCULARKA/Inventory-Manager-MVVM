using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public interface IIPAddressRepository : IRepository
	{
		void SetNewRangeOfIPAddresses(IEnumerable<IPAddress> range);

		IEnumerable<IPAddress> AllIPAddresses { get; }

		IQueryable<IPAddress> AllAvailableIPAddresses { get; }
	}
}
