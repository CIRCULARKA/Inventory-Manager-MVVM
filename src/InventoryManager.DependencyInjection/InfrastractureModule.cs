using InventoryManager.Infrastructure;
using InventoryManager.Infrastructure.Filtering;
using Ninject.Modules;

namespace InventoryManager.DependencyInjection
{
	public class InfrastractureModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IUserSession>().To<UserSession>().InSingletonScope();
			Bind<IDeviceFilter>().To<DeviceFilter>().InSingletonScope();
		}
	}
}
