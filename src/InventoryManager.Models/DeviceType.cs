using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Models
{
	public class DeviceType : ModelBase<DeviceType>
	{
		private List<DeviceType> _allDeviceTypes;

		public DeviceType() => _allDeviceTypes = DataContext.DeviceTypes.ToList();

		public int ID { get; set; }

		public string Name { get; set; }

		public override List<DeviceType> All() => _allDeviceTypes;
	}
}
