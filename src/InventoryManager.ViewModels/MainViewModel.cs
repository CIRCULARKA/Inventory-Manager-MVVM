using InventoryManager.Commands;
using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Infrastructure;
using System.Windows;
using System.Windows.Controls;
using System.Collections;

namespace InventoryManager.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		const string _githubUrl = "https://github.com/CIRCULARKA/Inventory-Manager-MVVM";

		const string _authorName = "Гачегов Руслан. 318 П/1";

		private IEnumerable _mainViewTabs;

		public MainViewModel()
		{
			LoadTabItemsContent();

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
					NetworkConfigurationView = new ConfigureIPSettingsView();
					NetworkConfigurationView.DataContext =
						new ConfigureIPSettingsViewModel(new DefaultIPAddressRepository());
					NetworkConfigurationView.ShowDialog();
				}
			);
		}

		public IEnumerable MainViewTabs
		{
			get => _mainViewTabs;
			set
			{
				_mainViewTabs = value;
				OnPropertyChanged(nameof(MainViewTabs));
			}
		}

		public ConfigureIPSettingsView NetworkConfigurationView { get; private set; }

		public Command ShowAboutProgramDialogCommand { get; }

		public Command ShowSetIPMaskDialogCommand { get; }

		public Command ApplyIPMaskChangesCommand { get; }

		public void LoadTabItemsContent()
		{
			MainViewTabs = new TabItem[]
			{
				new TabItem()
				{
					Header = "Устройства",
					Content = ViewModelLinker.GetRegisteredPartialView(nameof(DevicesManagementView))
				},
				new TabItem()
				{
					Header = "Пользователи",
					Content = ViewModelLinker.GetRegisteredPartialView(nameof(UsersManagementView))
				},
				new TabItem()
				{
					Header = "Сертификаты",
					Content = ViewModelLinker.GetRegisteredPartialView(nameof(CertificatesManagementView))
				}
			};
		}
	}
}
