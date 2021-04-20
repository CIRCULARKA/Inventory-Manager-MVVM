using InventoryManager.Models;
using System.Collections.Generic;

namespace InventoryManager.Infrastructure
{
	interface IDeviceFilter
	{
		Dictionary<string, bool> Rules { get; }

		bool DoesMeetSearchingCriteria(Device device);

		bool DoesMeetFilteringCriteria(Device device);
	}
}
