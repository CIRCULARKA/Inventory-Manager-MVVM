using InventoryManager.Events;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;
using System.Linq;

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
					DevicesFilter.Criteria.First(c => c.DeviceTypeName == "Сервер").State = IsServersIncluded;
					DevicesFilter.Criteria.First(c => c.DeviceTypeName == "Коммутатор").State = IsSwitchesIncluded;
					DevicesFilter.Criteria.First(c => c.DeviceTypeName == "Персональный компьютер").State = IsPCIncluded;
					DevicesFilter.SearchQuery = InputtedSearchQuery;

					DeviceEvents.RaiseOnDeviceFilteringCriteriaChanged(
						DevicesFilter.Filter(
							ViewModelLinker.
								GetRegisteredViewModel<DevicesListViewModel>().
									AllDevices
						)
					);
				}
			);
		}

		public bool IsServersIncluded { get; set; } = true;

		public bool IsPCIncluded { get; set; } = true;

		public bool IsSwitchesIncluded { get; set; } = true;

		public string InputtedSearchQuery { get; set; } = "";

		public DeviceFilter DevicesFilter { get; }

		public Command FilterDevicesCommand { get; }
	}
}
