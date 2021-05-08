using Ninject;

namespace InventoryManager.DependencyInjection
{
	public static class NinjectKernel
	{
		private static StandardKernel _kernel;

		static NinjectKernel()
		{
			_kernel = new StandardKernel(
				new RepositoriesModule(),
				new ViewModelsModule(),
				new InfrastractureModule(),
				new ViewsModule()
			);
		}

		public static StandardKernel StandartNinjectKernel => _kernel;
	}
}
