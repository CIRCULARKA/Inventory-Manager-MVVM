namespace InventoryManager.Networking
{
	public interface INetworkConfigurationWriter
	{
		void WriteNetworkAddress(string address);

		void WriteMask(byte mask);
	}

}
