using InventoryManager.Models;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.Filtering
{
	public class DeviceFilter : IDeviceFilter
	{
		public DeviceFilter(List<DeviceFilteringCriteria> criteria)
		{
			Criteria = criteria;
		}

		// Implement a way to change criteria more easily than changing it by
		// index
		public List<DeviceFilteringCriteria> Criteria { get; }

		public string SearchQuery { get; set; } = "";

		public bool DoesMeetSearchingCriteria(Device device) =>
			device.DeviceType.Name.Contains(SearchQuery) ||
			device.NetworkName.Contains(SearchQuery) ||
			device.InventoryNumber.Contains(SearchQuery);

		public bool DoesMeetFilteringCriteria(Device device)
		{
			// Still need to figure out how to filter devices more effective way
			// Consider this as a temporary solution (at least it works)
			foreach (var criteria in Criteria)
			{
				if (criteria.State &&
					device.DeviceType.Name == criteria.DeviceTypeName)
					return true;
			}

			return false;
		}

		public bool DoesMeetSearchingAndFilteringCriteria(Device device) =>
			DoesMeetSearchingCriteria(device) && DoesMeetFilteringCriteria(device);

		public bool DoesMeetSearchAndFilteringCriteria(Device device)
		{
			if (DoesMeetSearchingCriteria(device))
				return DoesMeetFilteringCriteria(device);
			else return false;
		}

		public IEnumerable<Device> Filter(IEnumerable<Device> list) =>
			list.Where(d => DoesMeetSearchAndFilteringCriteria(d));
	}
}
