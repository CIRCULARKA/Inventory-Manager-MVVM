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

		string NetworkAddress { get; set; }

		/// <summary>
		/// Write changes made with Mask
		/// </summary>
		void WriteChanges(INetworkConfigurationWriter writer);
	}
}
