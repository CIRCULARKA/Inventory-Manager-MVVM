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

			RegisterViews();
			RegisterViewModels();
			LinkViewsWithViewModels();


#if RELEASE
			ViewModelLinker.GetRegisteredView<AuthorizationView>().Show();
#else
			ViewModelLinker.GetRegisteredView<MainView>().Show();
#endif
		}

		public void RegisterViewModels()
		{
			// !!!WARNING!!!
			// Order of registration is important for proper work
			// For example: if you register DeviceViewModel first and only then
			// AddDeviceViewModel then you'll get an exception because
			// DeviceViewModel calls GetRegisteredViewModel and try to get
			// AddDeviceViewModel wich is not yet registered.
			// Need to fix it somehow (sometime)

			var userRelatedRepo = new DefaultUserRelatedRepository();
			ViewModelLinker.RegisterViewModel(new AuthorizationViewModel(userRelatedRepo));
			ViewModelLinker.RegisterViewModel(new AddUserViewModel(userRelatedRepo));
			ViewModelLinker.RegisterViewModel(new UserViewModel(userRelatedRepo));

			var certificateRelatedRepo = new X509CertificateRelatedRepository();
			ViewModelLinker.RegisterViewModel(new CertificateViewModel(certificateRelatedRepo));

			var deviceRelatedRepo = new DefaultDeviceRelatedRepository();
			ViewModelLinker.RegisterViewModel(new AddDeviceViewModel(deviceRelatedRepo));
			ViewModelLinker.RegisterViewModel(new DeviceAccountViewModel(deviceRelatedRepo));
			ViewModelLinker.RegisterViewModel(new DeviceIPViewModel(deviceRelatedRepo));
			ViewModelLinker.RegisterViewModel(new ConfigureIPSettingsViewModel(new DefaultIPAddressRepository()));
			ViewModelLinker.RegisterViewModel(new DeviceSearchAndFilteringViewModel());
			ViewModelLinker.RegisterViewModel(new DeviceViewModel(deviceRelatedRepo));

			ViewModelLinker.RegisterViewModel(new MainViewModel());
		}

		public void RegisterViews()
		{
			ViewModelLinker.RegisterView(new AuthorizationView());

			ViewModelLinker.RegisterView(new MainView());
			ViewModelLinker.RegisterPartialView(new DevicesManagementView());
			ViewModelLinker.RegisterPartialView(new UsersManagementView());
			ViewModelLinker.RegisterPartialView(new CertificatesManagementView());

			ViewModelLinker.RegisterView(new AddCertificateView());

			ViewModelLinker.RegisterView(new AddDeviceAccountView());
			ViewModelLinker.RegisterView(new AddDeviceView());
			ViewModelLinker.RegisterView(new AddIPAddressView());
			ViewModelLinker.RegisterView(new AddDeviceAccountView());
			ViewModelLinker.RegisterView(new ConfigureIPSettingsView());
			ViewModelLinker.RegisterView(new DeviceMovementHistoryView());

			ViewModelLinker.RegisterView(new AddUserView());
		}

		public void LinkViewsWithViewModels()
		{
			ViewModelLinker.LinkViewWithViewModel(
				nameof(AuthorizationView),
				nameof(AuthorizationViewModel)
			);

			ViewModelLinker.LinkViewWithViewModel(
				nameof(MainView),
				nameof(MainViewModel)
			);

			ViewModelLinker.LinkViewWithViewModel(
				nameof(AddDeviceView),
				nameof(AddDeviceViewModel)
			);

			ViewModelLinker.LinkViewWithViewModel(
				nameof(AddIPAddressView),
				nameof(DeviceIPViewModel)
			);

			ViewModelLinker.LinkViewWithViewModel(
				nameof(AddDeviceAccountView),
				nameof(DeviceAccountViewModel)
			);

			ViewModelLinker.LinkPartialViewWithViewModel(
				nameof(UsersManagementView),
				nameof(UserViewModel)
			);

			ViewModelLinker.LinkViewWithViewModel(
				nameof(AddUserView),
				nameof(AddUserViewModel)
			);

			ViewModelLinker.LinkPartialViewWithViewModel(
				nameof(DevicesManagementView),
				nameof(DeviceViewModel)
			);

			ViewModelLinker.LinkPartialViewWithViewModel(
				nameof(CertificatesManagementView),
				nameof(CertificateViewModel)
			);
		}
	}
}
