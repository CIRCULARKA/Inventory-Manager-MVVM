using InventoryManager.Models;
using System.Collections.Generic;

namespace InventoryManager.Infrastructure
{
	public interface INetworkConfigurator
	{
		/// <summary>
		/// Range of IP addresses calculated considering
		/// <see cref="CurrentMask" /> and <see cref="NetworkAddress" />
		/// </summary>
		IEnumerable<IPAddress> IPAddresses { get; }

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
