using InventoryManager.Models;
using Ninject.Modules;

namespace InventoryManager.DependencyInjection
{
	public class DependencyBinder : NinjectModule
	{
		public override void Load()
		{
			Bind<IDeviceRelatedRepository>().To<DefaultDeviceRelatedRepository>();
			Bind<IUserRelatedRepository>().To<DefaultUserRelatedRepository>();
			Bind<ICertificateRelatedRepository>().To<DefaultCertificateRelatedRepository>();
		}
	}
}
