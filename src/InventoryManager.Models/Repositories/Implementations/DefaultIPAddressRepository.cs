using InventoryManager.Data;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public class DefaultIPAddressRepository : IIPAddressRepository
	{
		BaseDbContext DataContext { get; } = new DefaultDbContext();

		public void AddIPAddress(IPAddress newIP) =>
			DataContext.IPAddresses.Add(newIP);

		public void RemoveIPAddress(IPAddress IPtoRemove) =>
			DataContext.IPAddresses.Remove(IPtoRemove);

		public void UpdateIPAddress(IPAddress IPtoUpdate) =>
			DataContext.IPAddresses.Update(IPtoUpdate);

		public IEnumerable<IPAddress> AllIPAddresses =>
			DataContext.IPAddresses.ToList();

		public void SaveChanges() => DataContext.SaveChanges();
	}
}
