using Xunit;
using InventoryManager.Infrastructure;
using System.Linq;

namespace InventoryManager.Tests
{
	public class NetworkConfiguratorTests
	{
		[Fact]
		public void IsFirstHostGeneratedProperly()
		{
			// Assert
			var configurator = new NetworkConfigurator();

			var networkAddress = "192.168.33.72";

			// Act
			configurator.Mask = 26;
			configurator.NetworkAddress = networkAddress;

			var result1 = configurator.FirstHost;

			configurator.Mask = 30;
			configurator.NetworkAddress = "192.168.54.1";

			var result2 = configurator.FirstHost;

			Assert.Equal("192.168.33.65", result1);
			Assert.Equal("192.168.54.1", result2);
		}
	}
}
