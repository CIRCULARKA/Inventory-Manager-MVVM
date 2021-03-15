using InventoryManager.ViewModels;
using InventoryManager.Views;

namespace InventoryManager.Infrastructure
{
	public class Bootstrapper
	{
		public void LinkViewWithViewModel(ViewBase view, ViewModelBase viewModel) =>
			view.DataContext = viewModel;
	}
}
