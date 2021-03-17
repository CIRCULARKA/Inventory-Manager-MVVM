using InventoryManager.ViewModels;
using InventoryManager.Views;
using System.Collections.Generic;

namespace InventoryManager.Infrastructure
{
	/// <summary>
	/// Class for linking views with view models for farther bindings
	/// </summary>
	public class ViewModelLinker
	{
		private static Dictionary<string, ViewBase> _registeredViews { get; } =
			new Dictionary<string, ViewBase>();

		private static Dictionary<string, ViewModelBase> _registeredViewModels { get; } =
			new Dictionary<string, ViewModelBase>();

		public static void RegisterView<T>(T view) where T : ViewBase
		{
			try { _registeredViews.Add(view.GetType().Name, view); }
			catch { }
		}

		public static void RegisterViewModel<T>(T viewModel) where T : ViewModelBase
		{
			try { _registeredViewModels.Add(viewModel.GetType().Name, viewModel); }
			catch { }
		}

		/// <summary>
		/// You can link only one view model for same view.
		/// Repeated call of this method with similar view will override previous changes.
		/// View and view model must be registered with <see href="Register" /> methods before
		/// being linked
		/// </summary>
		public static void LinkViewWithViewModel(string viewName, string viewModelName) =>
			_registeredViews[viewName].DataContext = _registeredViewModels[viewModelName];
	}
}
