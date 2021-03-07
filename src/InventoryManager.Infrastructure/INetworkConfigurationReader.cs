using System.Configuration;

namespace InventoryManager.Infrastructure
{
	public interface INetworkConfigurationReader
	{
		byte GetMaskFromConfiguration();

		string GetNetworkAddressFromConfiguration();
	}
}
