using System.Configuration;

namespace InventoryManager.Networking
{
	public class XMLNetworkConfigurationWriter : INetworkConfigurationWriter
	{
		private Configuration _exeConfiguration =
			ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

		public void WriteNetworkAddress(string address)
		{
			_exeConfiguration.AppSettings.Settings["networkAddress"].Value = address;
			_exeConfiguration.Save();

			ConfigurationManager.RefreshSection("appSettings");
		}

		public void WriteMask(byte mask)
		{
			_exeConfiguration.AppSettings.Settings["networkMask"].Value = mask.ToString();
			_exeConfiguration.Save();

			ConfigurationManager.RefreshSection("appSettings");
		}
	}
}
