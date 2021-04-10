using InventoryManager.ViewModels;
using System;

namespace InventoryManager.Events
{
	public static class ViewModelEvents
	{
		public static event Action<ViewModelBase> OnViewModelInitiated;

		public static void RaiseOnViewModelInitiated(ViewModelBase vm) =>
			OnViewModelInitiated?.Invoke(vm);
	}
}
