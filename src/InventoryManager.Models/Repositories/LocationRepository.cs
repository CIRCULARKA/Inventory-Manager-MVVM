using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Models
{
	public class LocationRepository : RepositoryBase<Cabinet>
	{
		public override IEnumerable<Cabinet> All =>
			DataContext.Cabinets.Include(c => c.Housing).ToList();

		public IEnumerable<Housing> AllHousings =>
			DataContext.Housings.ToList();

		public IEnumerable<Cabinet> AllCabinets =>
			DataContext.Cabinets.ToList();
	}
}
