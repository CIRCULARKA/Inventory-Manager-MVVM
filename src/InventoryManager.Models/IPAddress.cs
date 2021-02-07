using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public class IPAddress : ModelBase<IPAddress>
	{
		private List<IPAddress> _allIPAddresses;

		public IPAddress() =>
			_allIPAddresses = DataContext.IPAddresses.ToList();

		public int ID { get; set; }

		public string Address { get; set; }

		public int DeviceID { get; set; }

		public Device Device { get; set; }

		public override List<IPAddress> All() => _allIPAddresses;
	}
}
