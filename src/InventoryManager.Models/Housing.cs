using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public class Housing : ModelBase<Housing>
	{
		private List<Housing> _allHousings;

		public Housing() =>
			_allHousings = DataContext.Housings.ToList();

		public int ID { get; set; }

		public string Name { get; set; }

		public override List<Housing> All() => _allHousings;
	}
}
