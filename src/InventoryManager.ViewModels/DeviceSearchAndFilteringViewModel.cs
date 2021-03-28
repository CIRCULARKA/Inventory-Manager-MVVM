using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;
using System.Collections.Generic;
using System;

namespace InventoryManager.ViewModels
{
	public class DeviceSearchAndFilteringViewModel : ViewModelBase
	{
		public DeviceSearchAndFilteringViewModel(DeviceFilter filter)
		{
			DevicesFilter = filter;

			FilterDevicesCommand = RegisterCommandAction(
				(obj) =>
				{
					DevicesFilter.IncludeServers = IsServersIncluded;
					DevicesFilter.IncludeSwitches = IsSwitchesIncluded;
					DevicesFilter.IncludePC = IsPCIncluded;

					OnFilterCriteriaChanged?.Invoke(
						DevicesFilter.GetFilteredDevicesList(
							ViewModelLinker.
								GetRegisteredViewModel<DevicesListViewModel>().
									AllDevices
						)
					);
				}
			);
		}

		public Action<IEnumerable<Device>> OnFilterCriteriaChanged;

		public bool IsServersIncluded { get; set; } = true;

		public bool IsPCIncluded { get; set; } = true;

		public bool IsSwitchesIncluded { get; set; } = true;

		public DeviceFilter DevicesFilter { get; }

		public Command FilterDevicesCommand { get; }
	}
}
