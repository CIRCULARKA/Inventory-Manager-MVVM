using InventoryManager.Views;
using InventoryManager.Events;
using InventoryManager.ViewModels;
using System.Collections.Generic;

namespace InventoryManager.Infrastructure
{
	/// <summary>
	/// Class for linking views with view models for farther bindings
	/// </summary>
	public static class ViewModelLinker
	{
		private static Dictionary<string, ViewBase> _registeredViews { get; } =
			new Dictionary<string, ViewBase>();

		private static Dictionary<string, System.Windows.Controls.UserControl> _registeredPartialViews { get; } =
			new Dictionary<string, System.Windows.Controls.UserControl>();

		private static Dictionary<string, ViewModelBase> _registeredViewModels { get; } =
			new Dictionary<string, ViewModelBase>();

		static ViewModelLinker()
		{
			ViewModelEvents.OnViewModelInitiated += vm =>
				RegisterCreatedViewModel(vm);
		}

		public static void RegisterView<T>(T view) where T : ViewBase
		{
			try { _registeredViews.Add(view.GetType().Name, view); }
			catch { }
		}

		private static void RegisterCreatedViewModel<T>(T vm) where T : ViewModelBase
		{
			try { _registeredViewModels.Add(vm.GetType().Name, vm); }
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

		public static T GetRegisteredViewModel<T>() where T : ViewModelBase =>
			_registeredViewModels[typeof(T).Name] as T;

		public static T GetRegisteredView<T>() where T : ViewBase, new() =>
			_registeredViews[typeof(T).Name] as T;

		public static T GetRegisteredPartialView<T>()
			where T : System.Windows.Controls.UserControl =>
			_registeredPartialViews[typeof(T).Name] as T;
	}
}
