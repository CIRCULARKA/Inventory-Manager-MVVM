using InventoryManager.Models;
using InventoryManager.Infrastructure;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
	public class DeviceFilter : IDeviceFilter
	{
		public bool IncludeServers { get; set; } = true;

		public bool IncludePC { get; set; } = true;

		public bool IncludeSwitches { get; set; } = true;

		public string SearchQuery { get; set; } = "";

		public bool DoesMeetSearchingCriteria(Device device) =>
			device.DeviceType.Name.Contains(SearchQuery) ||
			device.NetworkName.Contains(SearchQuery) ||
			device.InventoryNumber.Contains(SearchQuery);

		public bool DoesMeetFilteringCriteria(Device device)
		{
			if (device.DeviceType.Name == "Сервер" && IncludeServers)
				return true;
			if (device.DeviceType.Name == "Персональный компьютер" && IncludePC)
				return true;
			if (device.DeviceType.Name == "Коммутатор" && IncludeSwitches)
				return true;
			return false;
		}

		public bool DoesMeetSearchingAndFilteringCriteria(Device device) =>
			DoesMeetSearchingCriteria(device) && DoesMeetFilteringCriteria(device);

		public IEnumerable<Device> GetFilteredDevicesList(IEnumerable<Device> list) =>
			list.Where(d => DoesMeetSearchAndFilteringCriteria(d));

		public bool DoesMeetSearchAndFilteringCriteria(Device device)
		{
			if (DoesMeetSearchingCriteria(device))
				return DoesMeetFilteringCriteria(device);
			else return false;
		}
	}
}
