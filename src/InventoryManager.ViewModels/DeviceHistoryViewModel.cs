using InventoryManager.Models;
using InventoryManager.Views;
using InventoryManager.Commands;
using InventoryManager.Extensions;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
	public class DeviceHistoryViewModel : ViewModelBase, IDeviceMovementHistoryViewModel
	{
		private IEnumerable<DeviceMovementHistoryNote> _allDeviceHistoryNotes;

		public DeviceHistoryViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			ShowDeviceMovementHistoryCommand = RegisterCommandAction(
				(obj) =>
				{
					RefreshSelectedDeviceHistory();
					var _historyView = new DeviceMovementHistoryView();
					_historyView.DataContext = ResolveDependency<IDeviceMovementHistoryViewModel>();
					_historyView.Title = $"История перемещений {SelectedDevice.InventoryNumber}";
					_historyView.ShowDialog();
				},
				(obj) => SelectedDevice != null
			);
		}
		private IDeviceRelatedRepository Repository { get; }

		public Device SelectedDevice =>
			(ResolveDependency<IDevicesListViewModel>() as DevicesListViewModel).
				SelectedDevice;

		public Command ShowDeviceMovementHistoryCommand { get; set; }

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
