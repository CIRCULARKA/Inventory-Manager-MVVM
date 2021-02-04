using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Models
{
	public class DeviceType : ModelBase<DeviceType>
	{
		public int ID { get; set; }

		public string Name { get; set; }

		public override List<DeviceType> All() =>
			DataContext.DeviceTypes.AsNoTracking().ToList();
	}
}
