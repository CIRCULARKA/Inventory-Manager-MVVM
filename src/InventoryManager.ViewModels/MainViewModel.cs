using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Events;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using Ninject;
using static InventoryManager.DependencyInjection.NinjectKernel;

namespace InventoryManager.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		const string _githubUrl = "https://github.com/CIRCULARKA/Inventory-Manager-MVVM";

		const string _authorName = "Гачегов Руслан. 318 П/1";

		private List<TabItem> _mainViewTabs;

		private TabItem _selectedTab;

		private TabItem _devicesTab =
			new TabItem
			{
				Header = "Устройства",
				Content = new DevicesManagementView() {
					DataContext = new DevicesManagementViewModel()
				}
			};

		private TabItem _usersTab =
			new TabItem
			{
				Header = "Пользователи",
				Content = new UsersManagementView() {
					DataContext = new UserViewModel(
						StandartNinjectKernel.Get<IUserRelatedRepository>()
					)
				}
			};

		private TabItem _certificatesTab =
			new TabItem
			{
				Header = "Сертификаты",
				Content = new CertificatesManagementView() {
					DataContext = new CertificateViewModel(
						StandartNinjectKernel.Get<ICertificateRelatedRepository>()
					)
				}
			};

		public MainViewModel()
		{
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
				(obj) => NetworkConfigurationView.ShowDialog(),
				(obj) => UserSession.IsAuthorizedUserAllowedTo(UserActions.ChangeNetworkSettings)
			);

			LogoutCommand = RegisterCommandAction(
				(obj) => UserEvents.RaiseOnUserLoggedOut()
			);

			LoadTabItemsForAuthorizedUser();
		}

		public List<TabItem> MainViewTabs
		{
			get => _mainViewTabs;
			set
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

		public ConfigureIPSettingsView NetworkConfigurationView { get; set; }

		public Command ShowAboutProgramDialogCommand { get; }

		public Command ShowSetIPMaskDialogCommand { get; }

		public Command ApplyIPMaskChangesCommand { get; }

		public Command LogoutCommand { get; }

		public void LoadTabItemsForAuthorizedUser()
		{
			MainViewTabs = new List<TabItem>();

			var devicesManagamentView = new DevicesManagementView();
			var devicesManagementViewModel = new DevicesManagementViewModel();
			devicesManagamentView.DataContext = devicesManagementViewModel;

			_devicesTab = new TabItem
			{
				Header = "Устройства",
				Content = new DevicesManagementView()
			};

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
