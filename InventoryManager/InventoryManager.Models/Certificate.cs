using System;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.Models
{
	public class Certificate : ModelBase<Certificate>
	{
		public int ID { get; set; }

		public string Subject { get; set; }

		public DateTime ValidFrom { get; set; }

		public DateTime ValidTo { get; set; }

		public string State =>
			ValidTo < DateTime.Now ? "Сертификат недействителен!" :
				$"Осталось дней: {(ValidTo - DateTime.Now).Days}";

		public override List<Certificate> All() =>
			DataContext.Certificates.ToList();

	}
}
