using System.Configuration;
using System.Xml;

namespace InventoryManager.Infrastructure
{
	public class XMLNetworkConfigurationWriter : INetworkConfigurationWriter
	{
		public void WriteNetworkAddress(string address) =>
			ConfigurationManager.AppSettings.Set("networkAddress", address);

		public void WriteMask(byte mask)
		{
			ConfigXmlDocument xmlConfigurator = new ConfigXmlDocument();
			xmlConfigurator.Load(@"D:\Inventory-Manager-MVVM\src\app.config");
			
			var maskNode = xmlConfigurator.SelectSingleNode("//add[@key='networkMask']") as XmlElement;
			maskNode?.SetAttribute("value", mask.ToString());
		}
	}
}
