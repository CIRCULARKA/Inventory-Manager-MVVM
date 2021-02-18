using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.Models
{
	public class DeviceAccount : ModelBase<DeviceAccount>
	{
		public int ID { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public int DeviceID { get; set; }

		public Device Device { get; set; }

		public override List<DeviceAccount> All() =>
			DataContext.DeviceAccounts.ToList();
	}
}
