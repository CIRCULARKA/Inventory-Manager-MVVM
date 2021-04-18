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

		public static T GetRegisteredViewModel<T>() where T : ViewModelBase =>
			_registeredViewModels[typeof(T).Name] as T;
	}
}
