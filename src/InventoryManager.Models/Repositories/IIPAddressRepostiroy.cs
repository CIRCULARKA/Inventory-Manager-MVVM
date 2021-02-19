using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Models
{
	public interface IIPAddressRepository : IRepository
	{
		void AddIPAddress(IPAddress newIP) =>
			DataContext.IPAddresses.Add(newIP);

		void RemoveIPAddress(IPAddress IPtoRemove) =>
			DataContext.IPAddresses.Remove(IPtoRemove);

		void UpdateIPAddress(IPAddress IPtoUpdate) =>
			DataContext.IPAddresses.Update(IPtoUpdate);

		IEnumerable<IPAddress> AllIPAddresses =>
			DataContext.IPAddresses.ToList();
	}
}
