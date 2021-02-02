using System.Collections.Generic;

namespace InventoryManager.Models
{
	public class Group : ModelBase<Group>
	{
		public int ID { get; set; }

		public string Name { get; set; }
	}
}
