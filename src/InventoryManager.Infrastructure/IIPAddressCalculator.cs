using InventoryManager.Models;
using System.Collections.Generic;

namespace InventoryManager.Infrastructure
{
	public interface INetworkConfigurator
	{
		IEnumerable<IPAddress> IPAddressesOfCurrentMask { get; }

		/// <summary>
		/// Setting this property will change range of IP addresses
		/// in <see cref="IPAddressesOfCurrentMask" />
		/// </summary>
		int CurrentMask { get; set; }

		/// <summary>
		/// Write changes made with Mask and resulted range of IP addresses
		/// </summary>
		void WriteChanges(INetworkConfigurationWriter writer);
	}
}
