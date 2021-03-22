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
			InitialList = allDevices.ToList();

			IncludeServers = true;
			IncludePC = true;
			IncludeSwitches = true;
		}

		public List<Device> InitialList { get; }

		public bool IncludeServers { get; set; }

		public bool IncludePC { get; set; }

		public bool IncludeSwitches { get; set; }

		public string SearchQuery { get; set; } = "";

		private IEnumerable<Device> FilterDevices() =>
			InitialList.Where(d => IsDeviceMeetsSearchAndFilteringCriteria(d));

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

		public IEnumerable<Device> FilteredDevicesList => FilterDevices();
	}
}
