using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.Models
{
	public class Cabinet : ModelBase<Cabinet>
	{
		public int ID { get; set; }

		public int HousingID { get; set; }

		public Housing Housing { get; set; }

		public string Name { get; set; }

		public override List<Cabinet> All() =>
			DataContext.Cabinets.ToList();
	}
}
