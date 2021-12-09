using System;

namespace InventoryManager.Models
{
	public class Cabinet
	{
		public Guid ID { get; set; }

		public string Name { get; set; }

		public Guid HousingID { get; set; }

		public Housing Housing { get; set; }
	}
}
