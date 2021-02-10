using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Models
{
	public class DeviceCabinet : ModelBase<DeviceCabinet>
	{
		public string DeviceInventoryNumber { get; set; }

		public Device Device { get; set; }

		public int CabinetID { get; set; }

		public Cabinet Cabinet { get; set; }

		public override List<DeviceCabinet> All() =>
			DataContext.DeviceCabinets.
			Include(dc => dc.Device).
			Include(dc => dc.Cabinet).
			ToList();
	}
}
