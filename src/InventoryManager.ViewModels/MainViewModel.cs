using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;

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
				Content = ViewModelLinker.GetRegisteredPartialView<DevicesManagementView>()
			};

		private TabItem _usersTab =
			new TabItem
			{
				Header = "Пользователи",
				Content = ViewModelLinker.GetRegisteredPartialView<UsersManagementView>()
			};

		private TabItem _certificatesTab =
			new TabItem
			{
				Header = "Сертификаты",
				Content = ViewModelLinker.GetRegisteredPartialView<CertificatesManagementView>()
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
				(obj) => NetworkConfigurationView.ShowDialog()
			);
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

		public ConfigureIPSettingsView NetworkConfigurationView =>
			ViewModelLinker.GetRegisteredView<ConfigureIPSettingsView>();

		public Command ShowAboutProgramDialogCommand { get; }

		public Command ShowSetIPMaskDialogCommand { get; }

		public Command ApplyIPMaskChangesCommand { get; }

		public void LoadTabItemsContent()
		{
			MainViewTabs = new List<TabItem>();

			if (AuthorizedUser.IsAllowedTo(UserActions.InspectDevices))
				MainViewTabs.Add(_devicesTab);
			if (AuthorizedUser.IsAllowedTo(UserActions.InspectUsers))
				MainViewTabs.Add(_usersTab);
			if (AuthorizedUser.IsAllowedTo(UserActions.InspectCertificates))
				MainViewTabs.Add(_certificatesTab);

			SelectFirstTab();
		}

		private void SelectFirstTab() =>
			SelectedTab = MainViewTabs[0];
	}
}
