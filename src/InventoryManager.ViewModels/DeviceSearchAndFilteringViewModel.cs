using System.Collections.Generic;
using InventoryManager.Models;
using InventoryManager.Commands;
using System;
using System.Linq;

namespace InventoryManager.ViewModels
{
	public class DeviceSearchAndFilteringViewModel : ViewModelBase
	{
		public DeviceSearchAndFilteringViewModel(IEnumerable<Device> allDevices)
		{
			AllDevices = allDevices;
		}

		public IEnumerable<Device> AllDevices { get; }

		public bool IncludeServers { get; set; }

		public bool IncludePC { get; set; }

		public bool IncludeSwitches { get; set; }

		public string SearchQuery { get; set; }

		private IEnumerable<Device> FilterDevices()
		{
			var result = AllDevices.
				Where(
					d => d.DeviceType.Name.Contains(SearchQuery) ||
						d.InventoryNumber.Contains(SearchQuery) ||
						d.NetworkName.Contains(SearchQuery)
				).AsQueryable();

			if (IncludeServers)
				result = result.Where(d => d.DeviceType.Name == "Сервер");
			if (IncludePC)
				result = result.Where(d => d.DeviceType.Name == "Персональный компьютер");
			if (IncludeSwitches)
				result = result.Where(d => d.DeviceType.Name == "Коммутатор");

			return result;
		}

		public IEnumerable<Device> FilteredDevicesList => FilterDevices();
	}
}
