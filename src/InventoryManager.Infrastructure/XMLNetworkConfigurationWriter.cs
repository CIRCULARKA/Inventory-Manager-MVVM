using System.Configuration;

namespace InventoryManager.Infrastructure
{
	public class XMLNetworkConfigurationWriter : INetworkConfigurationWriter
	{
		public void WriteNetworkAddress(string address) =>
			ConfigurationManager.AppSettings.Set("networkAddress", address);

		public void WriteMask(byte mask) =>
			ConfigurationManager.AppSettings.Set("networkMask", mask.ToString());
	}
}
