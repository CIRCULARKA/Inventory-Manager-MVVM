using InventoryManager.Infrastructure;
using Ninject.Modules;

namespace InventoryManager.DependencyInjection
{
	public class InfrastractureModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IUserSession>().To<UserSession>().InSingletonScope();
		}
	}
}
