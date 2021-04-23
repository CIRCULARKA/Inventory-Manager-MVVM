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
		}
	}
}
