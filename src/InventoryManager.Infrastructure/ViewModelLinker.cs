using InventoryManager.Views;
using InventoryManager.Events;
using InventoryManager.ViewModels;
using System.Collections.Generic;
using UserControl = System.Windows.Controls.UserControl;

namespace InventoryManager.Infrastructure
{
	/// <summary>
	/// Class for linking views with view models for farther bindings
	/// </summary>
	public static class ViewModelLinker
	{
		private static Dictionary<string, ViewModelBase> _registeredViewModels { get; } =
			new Dictionary<string, ViewModelBase>();

		static ViewModelLinker()
		{
			ViewModelEvents.OnViewModelInitiated += vm =>
				RegisterCreatedViewModel(vm);
		}

		private static void RegisterCreatedViewModel<T>(T vm) where T : ViewModelBase
		{
			try { _registeredViewModels.Add(vm.GetType().Name, vm); }
			catch { _registeredViewModels[vm.GetType().Name] = vm; }
		}

		/// <summary>
		/// You can link only one view model for same view.
		/// View model must be registered (to do it you should instantiate it at
		/// least once somewhere)
		/// </summary>
		public static void LinkViewWithViewModel(ViewBase view, string viewModelName) =>
			view.DataContext = _registeredViewModels[viewModelName];

		/// <summary>
		/// Same as <see href="LinkViewWithViewModel" /> but for views that
		/// inherited from <see href="UserControl" />
		/// </summary>
		public static void LinkPartialViewWithViewModel(UserControl view, string viewModelName) =>
			view.DataContext = _registeredViewModels[viewModelName];

		public static T GetRegisteredViewModel<T>() where T : ViewModelBase =>
			_registeredViewModels[typeof(T).Name] as T;
	}
}
