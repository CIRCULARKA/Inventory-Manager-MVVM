using System;
using System.Security.Cryptography.X509Certificates;

namespace InventoryManager.Models
{
	public class Certificate
	{
		public Guid ID { get; set; }

		public string Name { get; set; }

		public string SerialNumber { get; set; }

		public string Subject { get; set; }

		public string Issuer { get; set; }

		public DateTime ExpirationDate { get; set; }

		public string State =>
			ExpirationDate < DateTime.Now ? "Сертификат недействителен!" :
				$"Осталось дней: {(ExpirationDate - DateTime.Now).Days}";

		public static implicit operator Certificate(X509Certificate2 cert)
		{
			var result = new Certificate();
			result.Name = string.IsNullOrWhiteSpace(cert.FriendlyName) ? "Нет имени" : cert.FriendlyName;
			result.SerialNumber = cert.SerialNumber;
			result.Subject = cert.Subject.Remove(0, 3);
			result.Issuer = cert.Issuer.Remove(0, 3);
			result.ExpirationDate = DateTime.Parse(cert.GetExpirationDateString());

			return result;
		}
	}
}
