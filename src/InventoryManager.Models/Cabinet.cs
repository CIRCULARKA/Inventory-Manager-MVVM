using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Models
{
	public class Cabinet : ModelBase<Cabinet>
	{
		public int ID { get; set; }

		public string Name { get; set; }

		public override List<Cabinet> All() =>
			DataContext.Cabinets.ToList();

		/// <summary>
		/// All cabinets in specified housing
		/// </summary>
		public List<Cabinet> All(Housing housing)
		{
			var housingCabinets = DataContext.
				HousingCabinets.
				Where(hc => hc.HousingID == housing.ID);

			var result = new List<Cabinet>();
			foreach (var item in housingCabinets)
				result.Add(item.Cabinet);

			return result;
		}
	}
}
