using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public class HousingCabinet : ModelBase<HousingCabinet>
	{
		public int HousingID { get; set; }

		public Housing Housing { get; set; }

		public int CabinetID { get; set; }

		public Cabinet Cabinet { get; set; }

		public override List<HousingCabinet> All() =>
			DataContext.HousingCabinets.ToList();
	}
}
