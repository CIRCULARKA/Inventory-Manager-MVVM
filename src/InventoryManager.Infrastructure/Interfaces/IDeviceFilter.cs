using InventoryManager.Models;
using System.Collections.Generic;

namespace InventoryManager.Infrastructure.Filtering
{
	public interface IDeviceFilter
	{
		List<DeviceFilteringCriteria> Criteria { get; }

		string SearchQuery { get; set; }

		bool DoesMeetSearchingCriteria(Device device);

		bool DoesMeetFilteringCriteria(Device device);

		bool DoesMeetSearchingAndFilteringCriteria(Device device);

		IEnumerable<Device> Filter(IEnumerable<Device> list);
	}
}
