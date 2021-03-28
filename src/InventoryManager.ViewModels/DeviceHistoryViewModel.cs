using InventoryManager.Models;
using InventoryManager.Views;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;
using InventoryManager.Extensions;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
	public class DeviceHistoryViewModel : ViewModelBase
	{
		private IEnumerable<DeviceMovementHistoryNote> _allDeviceHistoryNotes;

		public DeviceHistoryViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			ShowDeviceMovementHistoryCommand = RegisterCommandAction(
				(obj) =>
				{
					RefreshSelectedDeviceHistory();
					DeviceMovementHistoryView.Title = $"История перемещений {SelectedDevice.InventoryNumber}";
					DeviceMovementHistoryView.ShowDialog();
				},
				(obj) => SelectedDevice != null
			);
		}
		private IDeviceRelatedRepository Repository { get; }

		public Device SelectedDevice =>
			ViewModelLinker.
				GetRegisteredViewModel<DevicesListViewModel>().
					SelectedDevice;

		public Command ShowDeviceMovementHistoryCommand { get; set; }

		public DeviceMovementHistoryView DeviceMovementHistoryView =>
			ViewModelLinker.
				GetRegisteredView<DeviceMovementHistoryView>();

		public IEnumerable<DeviceMovementHistoryNote> SelectedDeviceMovementHistoryNotes
		{
			get => _allDeviceHistoryNotes;
			set
			{
				_allDeviceHistoryNotes = value;
				OnPropertyChanged(nameof(SelectedDeviceMovementHistoryNotes));
			}
		}

		private void RefreshSelectedDeviceHistory() =>
			SelectedDeviceMovementHistoryNotes =
				Repository.GetAllDeviceHistoryNotes(SelectedDevice).ToList();
	}
}
