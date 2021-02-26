using System.Collections.Generic;
using InventoryManager.Models;
using System.Security.Cryptography.X509Certificates;

namespace InventoryManager.Extensions
{
	public static class X509StoreExtensions
	{
		public static List<Certificate> ToList(this X509Certificate2Collection store)
		{
			var result = new List<Certificate>();

			foreach (var cert in store)
				result.Add(cert);

			return result;
		}
	}
}
