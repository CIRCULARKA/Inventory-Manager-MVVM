using System.Configuration;

namespace InventoryManager.Infrastructure
{
	public class XMLNetworkConfigurationReader : INetworkConfigurationReader
	{
		private static XMLNetworkConfigurationReader _instance;

		public XMLNetworkConfigurationReader Instance
		{
			get
			{
				if (_instance == null)
					return new XMLNetworkConfigurationReader();
				return _instance;
			}
		}

		public byte GetMaskFromConfiguration() =>
			byte.Parse(ConfigurationManager.AppSettings["networkMask"]);

		public string GetNetworkAddressFromConfiguration() =>
			ConfigurationManager.AppSettings["networkAddress"];
	}
}
