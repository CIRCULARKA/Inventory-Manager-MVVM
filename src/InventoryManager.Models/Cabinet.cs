using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.Models
{
	public class Cabinet : ModelBase<Cabinet>
	{
		private List<Cabinet> _allCabinets;

		public Cabinet() => _allCabinets = DataContext.Cabinets.ToList();

		public int ID { get; set; }

		public int HousingID { get; set; }

		public Housing Housing { get; set; }

		public string Name { get; set; }

		public override List<Cabinet> All() => _allCabinets;
	}
}
