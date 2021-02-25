using InventoryManager.Commands;
using InventoryManager.Views;
using System.Windows;

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
						$"Программа для управления инвентарём колледжа.\nАвтор: {_authorName}",
						"О программе"
					);
				}
			);

			ShowSetIPMaskDialogCommand = RegisterCommandAction(
				(obj) =>
				{
					SetIPMaskView = new SetIPMaskView();
					SetIPMaskView.DataContext = this;
					SetIPMaskView.ShowDialog();
				}
			);
		}

		public SetIPMaskView SetIPMaskView { get; private set; }

		public Command ShowAboutProgramDialogCommand { get; }

		public Command ShowSetIPMaskDialogCommand { get; }
	}
}
