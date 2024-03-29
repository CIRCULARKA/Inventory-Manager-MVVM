using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Events;
using InventoryManager.Commands;
using InventoryManager.UsersAccess;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using Ninject;
using static InventoryManager.DependencyInjection.NinjectKernel;

namespace InventoryManager.ViewModels
{
	public class MainViewModel : ViewModelBase, IMainViewModel, IUserSessionViewModel
	{
		const string _githubUrl = "https://github.com/CIRCULARKA/Inventory-Manager-MVVM";

		const string _authorName = "Гачегов Руслан. 318 П/1";

		private List<TabItem> _mainViewTabs;

		private TabItem _selectedTab;

		private TabItem _devicesTab;

		private TabItem _usersTab;

		private TabItem _certificatesTab;

		public MainViewModel(IUserSession userSession)
		{
			UserSession = userSession;

			_devicesTab = new TabItem
			{
				Header = "Устройства",
				Content = new DevicesManagementView()
				{
					DataContext = new DevicesManagementViewModel()
				}
			};

			_usersTab = new TabItem
			{
				Header = "Пользователи",
				Content = new UsersManagementView()
				{
					DataContext = (ResolveDependency<IUserViewModel>())
						as UserViewModel
				}
			};

			_certificatesTab = new TabItem
			{
				Header = "Сертификаты",
				Content = new CertificatesManagementView()
				{
					DataContext = ResolveDependency<ICertificateViewModel>()
				}
			};

			ShowAboutProgramDialogCommand = RegisterCommandAction(
				(obj) =>
				{
					MessageBox.Show(
						$"Программа для управления инвентарём колледжа.\nАвтор: {_authorName}\n" +
						$"Репозиторий: {_githubUrl}",
						"О программе"
					);
				}
			);

			ShowSetIPMaskDialogCommand = RegisterCommandAction(
				(obj) =>
				{
					var _ipSettingsView = new ConfigureIPSettingsView();

					var _ipSettingsViewModel = new ConfigureIPSettingsViewModel(
						StandartNinjectKernel.Get<IIPAddressRepository>()
					);
					_ipSettingsViewModel.RelatedView = _ipSettingsView;

					_ipSettingsView.DataContext = _ipSettingsViewModel;

					_ipSettingsView.Show();

				},
				(obj) => UserSession.IsAuthorizedUserAllowedTo(UserActions.ChangeNetworkSettings)
			);

			LogoutCommand = RegisterCommandAction(
				(obj) => UserEvents.RaiseOnUserLoggedOut()
			);

			ShowReportsMasterViewCommand = RegisterCommandAction(
				(obj) =>
				{
					var reportsMasterView = new ReportsMasterView();
					reportsMasterView.DataContext = ResolveDependency<IReportsMasterViewModel>();
					reportsMasterView.ShowDialog();
				}
			);

			LoadViewsForAuthorizedUser();
		}

		public IUserSession UserSession { get; }

		public List<TabItem> MainViewTabs
		{
			get => _mainViewTabs;
			private set
			{
				_mainViewTabs = value;
				OnPropertyChanged(nameof(MainViewTabs));
			}
		}

		public TabItem SelectedTab
		{
			get => _selectedTab;
			set
			{
				_selectedTab = value;
				OnPropertyChanged(nameof(SelectedTab));
			}
		}

		public Command ShowAboutProgramDialogCommand { get; }

		public Command ShowSetIPMaskDialogCommand { get; }

		public Command LogoutCommand { get; }

		public Command ShowReportsMasterViewCommand { get; }

		public void LoadViewsForAuthorizedUser()
		{
			MainViewTabs = new List<TabItem>();

			if (UserSession.IsAuthorizedUserAllowedTo(UserActions.InspectDevices))
				MainViewTabs.Add(_devicesTab);
			if (UserSession.IsAuthorizedUserAllowedTo(UserActions.InspectUsers))
				MainViewTabs.Add(_usersTab);
			if (UserSession.IsAuthorizedUserAllowedTo(UserActions.InspectCertificates))
				MainViewTabs.Add(_certificatesTab);

			SelectFirstTab();
		}

		private void SelectFirstTab() =>
			SelectedTab = MainViewTabs[0];
	}
}
