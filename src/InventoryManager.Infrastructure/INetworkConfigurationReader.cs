namespace InventoryManager.Infrastructure
{
	public interface INetworkConfigurationReader
	{
		byte GetMaskFromConfiguration();

		string GetNetworkAddressFromConfiguration();
	}
}
