using System.Configuration;

namespace InventoryManager.Infrastructure
{
	public interface INetworkConfigurationReader
	{
		static byte GetMaskFromConfiguration() =>
			byte.Parse(ConfigurationManager.AppSettings["networkMask"]);

		static string GetNetworkAddressFromConfiguration() =>
			ConfigurationManager.AppSettings["networkAddress"];
	}
}
