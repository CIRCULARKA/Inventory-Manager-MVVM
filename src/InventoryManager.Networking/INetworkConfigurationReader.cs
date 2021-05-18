namespace InventoryManager.Networking
{
	public interface INetworkConfigurationReader
	{
		byte GetMaskFromConfiguration();

		string GetNetworkAddressFromConfiguration();
	}
}
