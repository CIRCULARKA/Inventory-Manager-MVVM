using System.Collections.Generic;
using InventoryManager.Models;
using System.Security.Cryptography.X509Certificates;

namespace InventoryManager.Extensions
{
	public static class X509StoreExtensions
	{
		public static List<Certificate> ToList(this X509Store store)
		{
			var result = new List<Certificate>();

			store.Open(OpenFlags.ReadOnly);
			foreach (var cert in store.Certificates)
				result.Add(cert);

			return result;
		}
	}
}
