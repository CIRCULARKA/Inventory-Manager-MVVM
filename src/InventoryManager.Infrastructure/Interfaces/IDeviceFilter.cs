using InventoryManager.Models;

namespace InventoryManager.Infrastructure
{
	interface IDeviceFilter
	{
		bool DoesMeetsSearchingCriteria(Device device);

		bool DoesMeetsFilteringCriteria(Device device);
	}
}
