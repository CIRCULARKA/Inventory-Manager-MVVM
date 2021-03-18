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

		private static Dictionary<string, System.Windows.Controls.UserControl> _registeredPartialViews { get; } =
			new Dictionary<string, System.Windows.Controls.UserControl>();

		private static Dictionary<string, ViewModelBase> _registeredViewModels { get; } =
			new Dictionary<string, ViewModelBase>();

		public static void RegisterView<T>(T view) where T : ViewBase
		{
			try { _registeredViews.Add(view.GetType().Name, view); }
			catch { }
		}

		public static void RegisterPartialView<T>(T view) where T : System.Windows.Controls.UserControl
		{
			try { _registeredPartialViews.Add(view.GetType().Name, view); }
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

		public static void LinkPartialViewWithViewModel(string viewName, string viewModelName) =>
			_registeredPartialViews[viewName].DataContext = _registeredViewModels[viewModelName];

		public static ViewModelBase GetRegisteredViewModel(string viewModelName) =>
			_registeredViewModels[viewModelName];

		public static T GetRegisteredView<T>() where T : ViewBase, new()
		{
			var viewName = typeof(T).Name;
			if (_registeredViews[viewName].IsLoaded)
				_registeredViews[viewName] = new T();
			return _registeredViews[viewName] as T;
		}

		public static System.Windows.Controls.UserControl GetRegisteredPartialView(string viewName) =>
			_registeredPartialViews[viewName];
	}
}
