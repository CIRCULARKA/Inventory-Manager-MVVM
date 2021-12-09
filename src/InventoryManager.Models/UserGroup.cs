using System;

namespace InventoryManager.Models
{
	public class UserGroup
	{
		public Guid ID { get; set; }

		public string Name { get; set; }

		public override string ToString() => Name;
	}
}
