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

		private const byte _maxMaskValue = 32;

		public NetworkConfigurator(INetworkConfigurationReader reader)
		{
			NetworkAddress = reader.GetNetworkAddressFromConfiguration();
			Mask = reader.GetMaskFromConfiguration();
		}

		public string NetworkAddress { get; set; }

		public byte Mask { get; set; }

		public IPAddress FirstHost
		{
			get
			{
				var result = new byte[_octetsAmount];
				for (int i = 0; i < _octetsAmount; i++)
					result[i] = (byte)(NetworkAddressOctets[i] & MaskOctets[i]);
				if (result[result.Length - 1] < 255) result[result.Length - 1]++;

				return new IPAddress { Address = GetAddressFromOctets(result), DeviceID = -1 };
			}
		}

		public IPAddress LastHost
		{
			get
			{
				var result = new byte[_octetsAmount];
				for (int i = 0; i < _octetsAmount; i++)
					result[i] = (byte)(NetworkAddressOctets[i] | ~MaskOctets[i]);
				if (result[result.Length - 1] != 0) result[result.Length - 1]--;

				return new IPAddress { Address = GetAddressFromOctets(result) };
			}
		}

		public IEnumerable<IPAddress> IPAddresses
		{
			get
			{
				var hostsAmount = (int)Math.Pow(2, _maxMaskValue - Mask) - 2;
				var firstHostBytes = GetOctetsFromAddress(FirstHost.Address);
				var lastHostBytes = GetOctetsFromAddress(LastHost.Address);

				var result = new List<IPAddress>(hostsAmount);

				var currentIP = firstHostBytes;
				result.Add(new IPAddress { Address = GetAddressFromOctets(currentIP) });
				for (int i = 0; i < hostsAmount - 1; i++)
				{
					for (int j = _octetsAmount - 1; j > 0; j--)
					{
						if (firstHostBytes[j] < lastHostBytes[j])
						{
							currentIP[j]++;
							result.Add(
								new IPAddress { Address = GetAddressFromOctets(currentIP) }
							);
							break;
						}
					}
				}

				return result;
			}
		}

		public void WriteMask(INetworkConfigurationWriter writer) => writer.WriteNetworkAddress(NetworkAddress);

		public void WriteNetworkAddress(INetworkConfigurationWriter writer) => writer.WriteMask(Mask);

		private byte[] MaskOctets
		{
			get
			{
				uint maskValue = ~(uint.MaxValue >> Mask);
				return BitConverter.GetBytes(maskValue).Reverse().ToArray();
			}
		}

		private byte[] NetworkAddressOctets => GetOctetsFromAddress(NetworkAddress);

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
