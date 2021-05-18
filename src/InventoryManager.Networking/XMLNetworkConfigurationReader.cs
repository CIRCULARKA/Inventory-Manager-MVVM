using System.Configuration;

namespace InventoryManager.Networking
{
	public class XMLNetworkConfigurationReader : INetworkConfigurationReader
	{
		public byte GetMaskFromConfiguration() =>
			byte.Parse(ConfigurationManager.AppSettings["networkMask"]);

		public string GetNetworkAddressFromConfiguration() =>
			ConfigurationManager.AppSettings["networkAddress"];
	}
}
