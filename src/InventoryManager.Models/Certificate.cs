using System;

namespace InventoryManager.Models
{
	public class Certificate : ModelBase<Certificate>
	{
		public string Subject { get; set; }

		public DateTime ValidFrom { get; set; }

		public DateTime ValidUntil { get; set; }
	}
}
