// #define RELEASE

using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.ViewModels;
using InventoryManager.Infrastructure;
using System.Windows;

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
			var usr = new User { UserGroupID = 3 };
			UserSession.AuthorizeUser(
				usr,
				UserRightsBuilder.GetUserRights(UserSession.GetUserAccessLevel(usr))
			);

			ViewModelLinker.GetRegisteredViewModel<MainViewModel>().LoadTabItemsContent();
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
			ViewModelLinker.RegisterViewModel(new AddDeviceAccountViewModel(deviceRelatedRepo));
			ViewModelLinker.RegisterViewModel(new DeviceLocationViewModel(deviceRelatedRepo));
			ViewModelLinker.RegisterViewModel(new DeviceSearchAndFilteringViewModel(new DeviceFilter()));
			ViewModelLinker.RegisterViewModel(new DevicesListViewModel(deviceRelatedRepo));
			ViewModelLinker.RegisterViewModel(new DeviceAccountsListViewModel(deviceRelatedRepo));
			ViewModelLinker.RegisterViewModel(new ConfigureIPSettingsViewModel(new DefaultIPAddressRepository()));
			ViewModelLinker.RegisterViewModel(new AddIPToDeviceViewModel(deviceRelatedRepo));
			ViewModelLinker.RegisterViewModel(new DeviceIPListViewModel(deviceRelatedRepo));
			ViewModelLinker.RegisterViewModel(new DevicesManagementViewModel());
			ViewModelLinker.RegisterViewModel(new DeviceHistoryViewModel(deviceRelatedRepo));

			ViewModelLinker.RegisterViewModel(new MainViewModel());
		}

		public void RegisterViews()
		{
			ViewModelLinker.RegisterView(new AuthorizationView());

			ViewModelLinker.RegisterView(new MainView());
			ViewModelLinker.RegisterPartialView(new DevicesManagementView());
			ViewModelLinker.RegisterPartialView(new UsersManagementView());
			ViewModelLinker.RegisterPartialView(new CertificatesManagementView());
			ViewModelLinker.RegisterPartialView(new DevicesListView());
			ViewModelLinker.RegisterPartialView(new DeviceIPListView());
			ViewModelLinker.RegisterPartialView(new DeviceAccountsListView());
			ViewModelLinker.RegisterPartialView(new DeviceLocationView());
			ViewModelLinker.RegisterPartialView(new DeviceSearchAndFilteringView());
			ViewModelLinker.RegisterPartialView(new DeviceHistoryView());

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

			ViewModelLinker.LinkPartialViewWithViewModel(
				nameof(DeviceAccountsListView),
				nameof(DeviceAccountsListViewModel)
			);

			ViewModelLinker.LinkViewWithViewModel(
				nameof(MainView),
				nameof(MainViewModel)
			);

			ViewModelLinker.LinkPartialViewWithViewModel(
				nameof(DeviceHistoryView),
				nameof(DeviceHistoryViewModel)
			);

			ViewModelLinker.LinkPartialViewWithViewModel(
				nameof(DeviceLocationView),
				nameof(DeviceLocationViewModel)
			);

			ViewModelLinker.LinkViewWithViewModel(
				nameof(ConfigureIPSettingsView),
				nameof(ConfigureIPSettingsViewModel)
			);

			ViewModelLinker.LinkViewWithViewModel(
				nameof(AddDeviceView),
				nameof(AddDeviceViewModel)
			);

			ViewModelLinker.LinkViewWithViewModel(
				nameof(AddIPAddressView),
				nameof(AddIPToDeviceViewModel)
			);

			ViewModelLinker.LinkPartialViewWithViewModel(
				nameof(DeviceIPListView),
				nameof(DeviceIPListViewModel)
			);

			ViewModelLinker.LinkViewWithViewModel(
				nameof(AddDeviceAccountView),
				nameof(AddDeviceAccountViewModel)
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
				nameof(DevicesManagementViewModel)
			);

			ViewModelLinker.LinkPartialViewWithViewModel(
				nameof(CertificatesManagementView),
				nameof(CertificateViewModel)
			);

			ViewModelLinker.LinkViewWithViewModel(
				nameof(DeviceMovementHistoryView),
				nameof(DeviceHistoryViewModel)
			);

			ViewModelLinker.LinkPartialViewWithViewModel(
				nameof(DevicesListView),
				nameof(DevicesListViewModel)
			);

			ViewModelLinker.LinkPartialViewWithViewModel(
				nameof(DeviceSearchAndFilteringView),
				nameof(DeviceSearchAndFilteringViewModel)
			);
		}
	}
}
