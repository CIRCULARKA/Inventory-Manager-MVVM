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
				To<DeviceLocationViewModel>();

			Bind<IDeviceIPListViewModel>().
				To<DeviceIPListViewModel>();

			Bind<IDeviceAccountsListViewModel>().
				To<DeviceAccountsListViewModel>();

			Bind<IDeviceMovementHistoryViewModel>().
				To<DeviceHistoryViewModel>();

			Bind<IAddDeviceViewModel>().
				To<AddDeviceViewModel>();

			Bind<IAddUserViewModel>().
				To<AddUserViewModel>();

			Bind<IAddDeviceAccountViewModel>().
				To<AddDeviceAccountViewModel>();

			Bind<ICertificateViewModel>().
				To<CertificateViewModel>();
		}
	}
}
