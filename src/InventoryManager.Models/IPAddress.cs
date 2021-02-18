using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public class IPAddress : ModelBase<IPAddress>
	{
		public int ID { get; set; }

		public string Address { get; set; }

		public int DeviceID { get; set; }

		public Device Device { get; set; }

		public override List<IPAddress> All() =>
			DataContext.IPAddresses.ToList();
	}
}
