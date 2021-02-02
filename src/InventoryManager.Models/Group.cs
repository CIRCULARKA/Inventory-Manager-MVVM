using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public class Group : ModelBase<Group>
	{
		public int ID { get; set; }

		public string Name { get; set; }

		public override List<Group> All() =>
			DataContext.Groups.ToList();
	}
}
