using InventoryManager.Filtering;
using InventoryManager.UsersAccess;
using System.Collections.Generic;
using Ninject.Modules;

namespace InventoryManager.DependencyInjection
{
	public class InfrastractureModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IUserSession>().To<UserSession>().InSingletonScope();
			Bind<IDeviceFilter>().To<DeviceFilter>().InSingletonScope().
				WithConstructorArgument(
					"criteria",
					new List<DeviceFilteringCriteria> {
						new DeviceFilteringCriteria("Сервер"),
						new DeviceFilteringCriteria("Коммутатор"),
						new DeviceFilteringCriteria("Персональный компьютер")
					}
				);
		}
	}
}
