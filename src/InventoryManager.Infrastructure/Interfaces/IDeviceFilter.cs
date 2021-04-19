using InventoryManager.Models;

namespace InventoryManager.Infrastructure
{
	interface IDeviceFilter
	{
		bool DoesMeetSearchingCriteria(Device device);

		bool DoesMeetFilteringCriteria(Device device);
	}
}
