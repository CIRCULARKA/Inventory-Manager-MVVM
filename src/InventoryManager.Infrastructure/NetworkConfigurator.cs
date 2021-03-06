using InventoryManager.Models;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;

namespace InventoryManager.Infrastructure
{
	public class NetworkConfigurator : INetworkConfigurator
	{
		private const int _octetsAmount = 4;

		private const int _binaryOcteteSize = 8;

		public string NetworkAddress { get; set; }

		public byte[] NetworkAddressOctets => GetOctetsFromAddress(NetworkAddress);

		public int Mask { get; set; }

		public string FirstHost
		{
			get
			{
				var result = new byte[_octetsAmount];
				for (int i = 0; i < _octetsAmount; i++)
					result[i] = (byte)(NetworkAddressOctets[i] & MaskOctets[i]);
				if (result[result.Length - 1] < 255) result[result.Length - 1]++;

				return GetAddressFromOctets(result);
			}
		}

		public string LastHost
		{
			get
			{
				var result = new byte[_octetsAmount];
				for (int i = 0; i < _octetsAmount; i++)
					result[i] = (byte)(NetworkAddressOctets[i] | ~MaskOctets[i]);
				if (result[result.Length - 1] != 0) result[result.Length - 1]--;

				return GetAddressFromOctets(result);
			}
		}

		public IEnumerable<InventoryManager.Models.IPAddress> IPAddresses => null;

		public void WriteChanges(INetworkConfigurationWriter writer) { }

		private byte[] MaskOctets
		{
			get
			{
				uint maskValue = ~(uint.MaxValue >> Mask);
				return BitConverter.GetBytes(maskValue).Reverse().ToArray();
			}
		}

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
