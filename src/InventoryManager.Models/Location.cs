using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public class Location : ModelBase<Location>
	{
		public int ID { get; set; }

		public int HousingID { get; set; }

		public Housing Housing { get; set; }

		public int CabinetID { get; set; }

		public Cabinet Cabinet { get; set; }

		public override List<Location> All() =>
			DataContext.Locations.ToList();
	}
}
