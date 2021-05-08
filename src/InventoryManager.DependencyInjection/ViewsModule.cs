using InventoryManager.Views;
using Ninject.Modules;

namespace InventoryManager.DependencyInjection
{
	public class ViewsModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IAuthorizationView>().
				To<AuthorizationView>().InSingletonScope();
		}
	}

}
