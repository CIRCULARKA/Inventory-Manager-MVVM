using InventoryManager.Data;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public class DefaultIPAddressRepository : IIPAddressRepository
	{
		BaseDbContext DataContext { get; } = new DefaultDbContext();

		public void SetNewRangeOfIPAddresses(IEnumerable<IPAddress> range)
		{
			DataContext.IPAddresses.RemoveRange(
				DataContext.IPAddresses
			);

			DataContext.IPAddresses.AddRange(range);
		}

		public IEnumerable<IPAddress> AllIPAddresses =>
			DataContext.IPAddresses.ToList();

		public void SaveChanges() => DataContext.SaveChanges();
	}
}
