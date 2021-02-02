using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public class DeviceConfiguration : ModelBase<DeviceConfiguration>
	{
		public int ID { get; set; }

		public string AccountName { get; set; }

		public string AccountPassword { get; set; }

		public override List<DeviceConfiguration> All() =>
			DataContext.DeviceConfigurations.ToList();
	}
}
