using Ninject;
using static InventoryManager.DependencyInjection.NinjectKernel;

namespace InventoryManager.DependencyInjection
{
	public class DependencyResolver
	{
		public static T Resolve<T>() =>
			StandartNinjectKernel.Get<T>();
	}
}
