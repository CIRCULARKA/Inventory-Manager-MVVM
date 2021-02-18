using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Models
{
	public class Housing : ModelBase<Housing>
	{
		public int ID { get; set; }

		public string Name { get; set; }

		public override List<Housing> All() =>
			DataContext.Housings.AsNoTracking().ToList();
	}
}
