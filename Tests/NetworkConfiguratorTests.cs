using Xunit;
using InventoryManager.Infrastructure;

namespace InventoryManager.Tests
{
	public class NetworkConfiguratorTests
	{
		[Fact]
		public void IsFirstHostGeneratedProperly()
		{
			// Assert
			var configurator = new NetworkConfigurator();

			// Act
			configurator.Mask = 26;
			configurator.NetworkAddress = "192.168.33.72";

			var result1 = configurator.FirstHost;

			configurator.Mask = 30;
			configurator.NetworkAddress = "192.168.54.1";

			var result2 = configurator.FirstHost;

			configurator.Mask = 16;
			configurator.NetworkAddress = "192.168.54.1";

			var result3 = configurator.FirstHost;

			configurator.Mask = 8;
			configurator.NetworkAddress = "192.168.54.1";

			var result4 = configurator.FirstHost;

			Assert.Equal("192.168.33.65", result1);
			Assert.Equal("192.168.54.1", result2);
			Assert.Equal("192.168.0.1", result3);
			Assert.Equal("192.0.0.1", result4);
		}


		[Fact]
		public void IsLastHostGeneratedProperly()
		{
			// Assert
			var configurator = new NetworkConfigurator();

			var networkAddress = "192.168.33.72";

			// Act
			configurator.Mask = 26;
			configurator.NetworkAddress = networkAddress;

			var result1 = configurator.LastHost;

			configurator.Mask = 30;
			configurator.NetworkAddress = "192.168.54.1";

			var result2 = configurator.LastHost;

			configurator.Mask = 16;
			configurator.NetworkAddress = "192.168.54.1";

			var result3 = configurator.LastHost;

			configurator.Mask = 8;
			configurator.NetworkAddress = "192.168.54.1";

			var result4 = configurator.LastHost;

			Assert.Equal("192.168.33.126", result1);
			Assert.Equal("192.168.54.2", result2);
			Assert.Equal("192.168.255.254", result3);
			Assert.Equal("192.255.255.254", result4);
		}
	}
}
