using System;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.Models
{
	public class Certificate : ModelBase<Certificate>
	{
		public string Subject { get; set; }

		public DateTime ValidFrom { get; set; }

		public DateTime ValidTo { get; set; }

		public override List<Certificate> All() => DataContext.Certificates.ToList();
	}
}
