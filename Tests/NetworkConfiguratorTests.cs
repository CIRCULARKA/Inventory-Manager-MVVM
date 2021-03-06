using Xunit;
using System.Collections.Generic;
using System.Linq;
using InventoryManager.Infrastructure;

namespace InventoryManager.Tests
{
	public class NetworkConfiguratorTests
	{
		[Fact]
		public void IsFirstHostGeneratedProperly()
		{
			// Assert
			INetworkConfigurator configurator = new NetworkConfigurator();

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

			Assert.Equal("192.168.33.65", result1.Address);
			Assert.Equal("192.168.54.1", result2.Address);
			Assert.Equal("192.168.0.1", result3.Address);
			Assert.Equal("192.0.0.1", result4.Address);
		}


		[Fact]
		public void IsLastHostGeneratedProperly()
		{
			// Assert
			INetworkConfigurator configurator = new NetworkConfigurator();

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

			Assert.Equal("192.168.33.126", result1.Address);
			Assert.Equal("192.168.54.2", result2.Address);
			Assert.Equal("192.168.255.254", result3.Address);
			Assert.Equal("192.255.255.254", result4.Address);
		}

		[Fact]
		public void IsIPRangeGeneratedProperly()
		{
			// Arrange
			INetworkConfigurator configurator = new NetworkConfigurator();
			var expectedRange1 = new string[]
			{
				"192.168.54.1",
				"192.168.54.2"
			};

			var expectedRange2 = new string[]
			{
				"192.168.54.1",
				"192.168.54.2",
				"192.168.54.3",
				"192.168.54.4",
				"192.168.54.5",
				"192.168.54.6",
				"192.168.54.7",
				"192.168.54.8",
				"192.168.54.9",
				"192.168.54.10",
				"192.168.54.11",
				"192.168.54.12",
				"192.168.54.13",
				"192.168.54.14"
			};

			// Act
			configurator.NetworkAddress = "192.168.54.1";
			configurator.Mask = 30;
			var result1 = configurator.IPAddresses.ToList();

			configurator.Mask = 28;
			var result2 = configurator.IPAddresses.ToList();

			// Assert
			Assert.Equal(2, result1.Count);
			for (int i = 0; i < result1.Count; i++)
				Assert.Equal(expectedRange1[i], result1[i].Address);

			Assert.Equal(14, result2.Count);
			for (int i = 0; i < result2.Count; i++)
				Assert.Equal(expectedRange2[i], result2[i].Address);
		}
	}
}
