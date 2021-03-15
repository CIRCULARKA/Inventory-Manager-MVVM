using System.Collections.Generic;
using InventoryManager.Models;
using InventoryManager.Commands;
using System;
using System.Linq;

namespace InventoryManager.ViewModels
{
	public class DeviceSearchAndFilteringViewModel : ViewModelBase
	{
		private bool _includeServers;

		private bool _includePC;

		private bool _includeSwitches;

		public DeviceSearchAndFilteringViewModel(IEnumerable<Device> allDevices)
		{
			AllDevices = allDevices;
		}

		public DeviceSearchAndFilteringViewModel() { }

		public IEnumerable<Device> AllDevices { get; }

		public event Action<IEnumerable<Device>> OnDevicesFilteringConditionsChanged;

		public bool IsServersIncluded
		{
			get => _includeServers;
			set
			{
				_includeServers = value;
				OnDevicesFilteringConditionsChanged?.Invoke(GetFilteredDevicesList());
			}
		}

		public bool IsPCIncluded
		{
			get => _includePC;
			set
			{
				_includePC = value;
				OnDevicesFilteringConditionsChanged?.Invoke(GetFilteredDevicesList());
			}
		}

		public bool IsSwitchesIncluded
		{
			get => _includeSwitches;
			set
			{
				_includeSwitches = value;
				OnDevicesFilteringConditionsChanged?.Invoke(GetFilteredDevicesList());
			}
		}

		public string InputtedSearchQuery { get; set; }

		public IEnumerable<Device> GetFilteredDevicesList()
		{
			var result = AllDevices.
				Where(
					d => d.DeviceType.Name.Contains(InputtedSearchQuery) ||
						d.InventoryNumber.Contains(InputtedSearchQuery) ||
						d.NetworkName.Contains(InputtedSearchQuery)
				).AsQueryable();

			if (IsServersIncluded)
				result = result.Where(d => d.DeviceType.Name == "Сервер");
			if (IsPCIncluded)
				result = result.Where(d => d.DeviceType.Name == "Персональный компьютер");
			if (IsSwitchesIncluded)
				result = result.Where(d => d.DeviceType.Name == "Коммутатор");

			return result.ToList();
		}
	}
}
