using InventoryManager.Models;
using InventoryManager.Infrastructure.Filtering;
using System.Collections.Generic;

namespace InventoryManager.Infrastructure
{
	interface IDeviceFilter
	{
		List<DeviceFilteringCriteria> Criteria { get; }

		string SearchQuery { get; set; }

		bool DoesMeetSearchingCriteria(Device device);

		bool DoesMeetFilteringCriteria(Device device);

		IEnumerable<Device> Filter(IEnumerable<Device> list);
	}
}
