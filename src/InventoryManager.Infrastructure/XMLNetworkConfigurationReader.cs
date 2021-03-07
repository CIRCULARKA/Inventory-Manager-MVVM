using System.Configuration;

namespace InventoryManager.Infrastructure
{
	public class XMLNetworkConfigurationReader : INetworkConfigurationReader
	{
		private static XMLNetworkConfigurationReader _instance;

		private static object _syncObj = new object();

		public XMLNetworkConfigurationReader Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_syncObj)
					{
						if (_instance == null)
							_instance = new XMLNetworkConfigurationReader();
					}
				}

				return _instance;
			}
		}

		public byte GetMaskFromConfiguration() =>
			byte.Parse(ConfigurationManager.AppSettings["networkMask"]);

		public string GetNetworkAddressFromConfiguration() =>
			ConfigurationManager.AppSettings["networkAddress"];
	}
}
