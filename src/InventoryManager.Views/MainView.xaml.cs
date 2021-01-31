using System.Windows;
using InventoryManager.ViewModels;

namespace InventoryManager.Views
{
	public partial class MainView : ViewBase
	{
		public MainView()
		{
			InitializeComponent();

			DataContext = new MainViewModel();
		}
	}
}
