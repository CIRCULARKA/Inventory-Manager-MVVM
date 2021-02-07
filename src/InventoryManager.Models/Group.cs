using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Models
{
	public class Group : ModelBase<Group>
	{
		private List<Group> _allGroups;

		public Group() => _allGroups = DataContext.Groups.ToList();

		public int ID { get; set; }

		public string Name { get; set; }

		public override List<Group> All() => _allGroups;
	}
}
