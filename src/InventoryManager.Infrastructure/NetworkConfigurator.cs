using InventoryManager.Models;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager.Infrastructure
{
	public class NetworkConfigurator : INetworkConfigurator
	{
		private const int _octetsAmount = 4;

		private const int _binaryOcteteSize = 8;

		public string NetworkAddress { get; set; }

		public int Mask { get; set; }

		public string FirstHost
		{
			get
			{
				return null;
			}
		}

		public IEnumerable<IPAddress> IPAddresses => null;

		public void WriteChanges(INetworkConfigurationWriter writer) { }

		private byte[] GetOctetsFromAddress(string address)
		{
			var addressParts = address.Split(".");
			var result = new byte[_octetsAmount];

			for (int i = 0; i < _octetsAmount; i++)
				result[i] = byte.Parse(addressParts[i]);

			return result;
		}

		private string GetAddressFromOctets(byte[] octets)
		{
			var dotsAmount = 3;
			var stringIPCapacity = _binaryOcteteSize * _octetsAmount + dotsAmount;

			var result = new StringBuilder(stringIPCapacity, stringIPCapacity);
			for (int i = 0; i < _octetsAmount; i++)
			{
				result.Append(octets[i]);
				if (i != _octetsAmount - 1) result.Append('.');
			}

			return result.ToString();
		}
	}
}
