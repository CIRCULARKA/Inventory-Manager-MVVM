namespace InventoryManager.Infrastructure
{
	public interface INetworkConfigurationWriter
	{
		void WriteNetworkAddress();

		void WriteMask();
	}

}
