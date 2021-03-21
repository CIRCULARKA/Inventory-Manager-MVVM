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

		private IEnumerable<Device> _filteredDevices;

		public DeviceSearchAndFilteringViewModel(IEnumerable<Device> allDevices)
		{
			AllDevices = allDevices;

			OnDevicesFilteringConditionsChanged += FilterDevices;
		}

		public IEnumerable<Device> AllDevices { get; }

		public event Action OnDevicesFilteringConditionsChanged;

		public bool IncludeServers
		{
			get => _includeServers;
			set
			{
				_includeServers = value;
				OnDevicesFilteringConditionsChanged?.Invoke();
			}
		}

		public bool IncludePC
		{
			get => _includePC;
			set
			{
				_includePC = value;
				OnDevicesFilteringConditionsChanged?.Invoke();
			}
		}

		public bool IncludeSwitches
		{
			get => _includeSwitches;
			set
			{
				_includeSwitches = value;
				OnDevicesFilteringConditionsChanged?.Invoke();
			}
		}

		public string SearchQuery { get; set; }

		private void FilterDevices()
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

			FilteredDevicesList = result;
		}

		public IEnumerable<Device> FilteredDevicesList
		{
			private set => _filteredDevices = value;
			get => _filteredDevices;
		}
	}
}
