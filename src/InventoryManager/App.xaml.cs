// #define RELEASE

using System.Windows;
using InventoryManager.Views;
using InventoryManager.ViewModels;
using InventoryManager.Models;
using InventoryManager.Infrastructure;

namespace InventoryManager
{
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs info)
		{
			base.OnStartup(info);

			RegisterViewModels();

#if RELEASE
			var authWindow = new AuthorizationView();
			authWindow.Show();
#else
			var mainView = new MainView();
			mainView.Show();
#endif
		}

		public void RegisterViewModels()
		{
			ViewModelLinker.RegisterViewModel(new MainViewModel());

			var userRelatedRepo = new DefaultUserRelatedRepository();
			ViewModelLinker.RegisterViewModel(new AuthorizationViewModel(userRelatedRepo));
			ViewModelLinker.RegisterViewModel(new UserViewModel(userRelatedRepo));

			ViewModelLinker.RegisterViewModel(new UserViewModel(userRelatedRepo));
			ViewModelLinker.RegisterViewModel(new AddUserViewModel(userRelatedRepo));

			var certificateRelatedRepo = new X509CertificateRelatedRepository();
			ViewModelLinker.RegisterViewModel(new CertificateViewModel(certificateRelatedRepo));

			var deviceRelatedRepo = new DefaultDeviceRelatedRepository();
			ViewModelLinker.RegisterViewModel(new DeviceViewModel(deviceRelatedRepo));
			ViewModelLinker.RegisterViewModel(new AddDeviceViewModel(deviceRelatedRepo));
			ViewModelLinker.RegisterViewModel(new DeviceAccountViewModel(deviceRelatedRepo));
			ViewModelLinker.RegisterViewModel(new DeviceIPViewModel(deviceRelatedRepo));

			ViewModelLinker.RegisterViewModel(new ConfigureIPSettingsViewModel(new DefaultIPAddressRepository()));

			ViewModelLinker.RegisterViewModel(new DeviceSearchAndFilteringViewModel());
		}
	}
}
