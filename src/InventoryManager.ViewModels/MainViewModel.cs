using InventoryManager.Commands;
using InventoryManager.Views;
using System.Windows;
using System.Collections;

namespace InventoryManager.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		const string _githubUrl = "https://github.com/CIRCULARKA/Inventory-Manager-MVVM";
		const string _authorName = "Гачегов Руслан. 318 П/1";

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
				(obj) =>
				{
					SetIPMaskView = new ConfigureIPSettingsView();
					SetIPMaskView.DataContext = this;
					SetIPMaskView.ShowDialog();
				}
			);

			ApplyIPMaskChangesCommand = RegisterCommandAction(
				(obj) =>
				{
					// TODO
				}
			);
		}

		public IEnumerable MainViewContent { get; set; }

		public ConfigureIPSettingsView SetIPMaskView { get; private set; }

		public Command ShowAboutProgramDialogCommand { get; }

		public Command ShowSetIPMaskDialogCommand { get; }

		public Command ApplyIPMaskChangesCommand { get; }
	}
}
