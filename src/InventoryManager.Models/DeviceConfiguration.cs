using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InventoryManager.Models
{
	public class DeviceConfiguration : ModelBase<DeviceConfiguration>
	{
		public int ID { get; set; }

		public int AccountID { get; set; }

		public Account Account { get; set; }

		public int IPAddressID { get; set; }

		public IPAddress IPAddress { get; set; }

		public override List<DeviceConfiguration> All() =>
			DataContext.DeviceConfigurations.Include(dc => dc.IPAddress).ToList();
	}
}
