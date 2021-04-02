using System.Collections.Generic;
using InventoryManager.Models;
using System.Linq;

namespace InventoryManager.ViewModels
{
	public class DeviceFilter : ViewModelBase
	{
		public bool IncludeServers { get; set; } = true;

		public bool IncludePC { get; set; } = true;

		public bool IncludeSwitches { get; set; } = true;

		public string SearchQuery { get; set; } = "";

		public IEnumerable<Device> GetFilteredDevicesList(IEnumerable<Device> list) =>
			list.Where(d => IsDeviceMeetsSearchAndFilteringCriteria(d));

		public bool IsDeviceMeetsSearchAndFilteringCriteria(Device device)
		{
			if ((device.DeviceType.Name.Contains(SearchQuery) ||
				device.NetworkName.Contains(SearchQuery) ||
				device.InventoryNumber.Contains(SearchQuery)))
			{
				if (device.DeviceType.Name == "Сервер" && IncludeServers)
					return true;
				if (device.DeviceType.Name == "Персональный компьютер" && IncludePC)
					return true;
				if (device.DeviceType.Name == "Коммутатор" && IncludeSwitches)
					return true;
				return false;
			} else return false;
		}
	}
}
