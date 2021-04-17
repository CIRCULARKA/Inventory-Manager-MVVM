using Ninject;

namespace InventoryManager.DependencyInjection
{
	public static class NinjectKernel
	{
		private static StandardKernel _kernel;

		static NinjectKernel()
		{
			_kernel = new StandardKernel(
				new DependencyBinder()
			);
		}

		public static StandardKernel Instance => _kernel;
	}
}
