using System;

namespace InventoryManager.Models
{
	public class Certificate
	{
		public int ID { get; set; }

		public string SerialNumber { get; set; }

		public string Subject { get; set; }

		public string Issuer { get; set; }

		public DateTime ExpirationDate { get; set; }

		public string State =>
			ExpirationDate < DateTime.Now ? "Сертификат недействителен!" :
				$"Осталось дней: {(ExpirationDate - DateTime.Now).Days}";
	}
}
