namespace InventoryManager.Infrastructure
{
	public interface INetworkConfigurationWriter
	{
		void WriteNetworkAddress(string address);

		void WriteMask(byte mask);
	}

}
