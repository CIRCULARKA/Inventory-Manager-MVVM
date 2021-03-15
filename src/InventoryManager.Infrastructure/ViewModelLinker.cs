using InventoryManager.ViewModels;
using InventoryManager.Views;

namespace InventoryManager.Infrastructure
{
	/// <summary>
	/// Class for linking views with view models for farther bindings
	/// </summary>
	public class ViewModelLinker
	{
		/// <summary>
		/// You can only link one view model to one view.
		/// Repeated call of this method with similar view will override previous changes
		/// </summary>
		public void LinkViewWithViewModel(ViewBase view, ViewModelBase viewModel) =>
			view.DataContext = viewModel;
	}
}
