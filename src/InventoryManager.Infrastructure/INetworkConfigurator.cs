using InventoryManager.Models;
using System.Collections.Generic;

namespace InventoryManager.Infrastructure
{
	public interface INetworkConfigurator
	{
		/// <summary>
		/// Range of IP addresses calculated considering
		/// <see cref="Mask" /> and <see cref="NetworkAddress" />
		/// </summary>
		IEnumerable<IPAddress> IPAddresses { get; }

		IPAddress FirstHost { get; }

		IPAddress LastHost { get; }

		byte Mask { get; set; }

		string NetworkAddress { get; set; }

		/// <summary>
		/// Write changes made with Mask into file, specified by <see cref="Writer" />
		/// </summary>
		void WriteChanges(INetworkConfigurationWriter writer);
	}
}
