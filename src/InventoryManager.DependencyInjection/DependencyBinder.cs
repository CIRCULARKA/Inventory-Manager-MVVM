using InventoryManager.Models;
using Ninject.Modules;

namespace InventoryManager.DependencyInjection
{
	public class DependencyBinder : NinjectModule
	{
		public override void Load()
		{
			Bind<IDeviceRelatedRepository>().
				To<DefaultDeviceRelatedRepository>().
					InSingletonScope();

			Bind<IIPAddressRepository>().
				To<DefaultIPAddressRepository>().
					InSingletonScope();

			Bind<IUserRelatedRepository>().
				To<DefaultUserRelatedRepository>().
					InSingletonScope();

			Bind<ICertificateRelatedRepository>().
				To<DefaultCertificateRelatedRepository>().
					InSingletonScope();
		}
	}
}
