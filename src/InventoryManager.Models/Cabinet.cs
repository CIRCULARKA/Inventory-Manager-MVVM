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
		// Temporary comment until I figure out how to implement this method with HousingCabinet
		// public List<Cabinet> All(Housing housing) =>
		// 	DataContext.
		// 	Cabinets.
		// 	Include(c => c.Housing).
		// 	Where(c => c.HousingID == housing.ID).ToList();
	}
}
