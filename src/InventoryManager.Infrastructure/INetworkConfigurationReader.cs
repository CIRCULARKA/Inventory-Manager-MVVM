namespace InventoryManager.Infrastructure
{
	public interface INetworkConfigurationReader
	{
		byte GetMaskFromConfiguration();

		byte GetNetworkAddressFromConfiguration();
	}
}
