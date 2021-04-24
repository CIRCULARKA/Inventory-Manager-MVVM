using InventoryManager.ViewModels;
using Ninject.Modules;

namespace InventoryManager.DependencyInjection
{
	public class ViewModelsModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IMainViewModel>().
				To<MainViewModel>().InTransientScope();

			Bind<IDevicesListViewModel>().
				To<DevicesListViewModel>().InSingletonScope();

			Bind<IDeviceSearchAndFilteringViewModel>().
				To<DeviceSearchAndFilteringViewModel>().InSingletonScope();

			Bind<IDeviceLocationViewModel>().
				To<DeviceLocationViewModel>().InSingletonScope();

			Bind<IDeviceIPListViewModel>().
				To<DeviceIPListViewModel>().InSingletonScope();

			Bind<IDeviceAccountsListViewModel>().
				To<DeviceAccountsListViewModel>().InSingletonScope();

			Bind<IDeviceMovementHistoryViewModel>().
				To<DeviceHistoryViewModel>().InSingletonScope();

			Bind<IAddDeviceViewModel>().
				To<AddDeviceViewModel>().InTransientScope();
		}
	}
}
