using InventoryManager.Models;
using InventoryManager.Views;
using InventoryManager.Events;
using InventoryManager.Commands;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.ViewModels
{
	public class SoftwareListViewModel : ViewModelBase, ISoftwareListViewModel
	{
		public SoftwareListViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			ShowAddSoftwareViewCommand = RegisterCommandAction(
				(obj) =>
				{
					var _addSoftwareView = new AddSoftwareView();
					_addSoftwareView.DataContext = ResolveDependency<IAddSoftwareViewModel>()
						as AddSoftwareViewModel;
					_addSoftwareView.ShowDialog();

				},
				(obj) => SelectedDevice != null
			);

			RemoveSoftwareCommand = RegisterCommandAction(
				(obj) => Repository.RemoveSoftware(SelectedSoftware),
				(obj) => SelectedSoftware != null
			);

			DeviceEvents.OnDeviceSelectionChanged += (device) =>
			{
				if (device != null)
				{
					Repository.GetAllDeviceSoftware(
						SelectedDevice
					).ToList();
				}
			};
		}

		private IDeviceRelatedRepository Repository { get; }

		public Device SelectedDevice =>
			(ResolveDependency<IDevicesListViewModel>() as DevicesListViewModel).
				SelectedDevice;

		public IEnumerable<Software> SelectedDeviceSoftware { get; set; }

		public Software SelectedSoftware { get; set; }

		public Command ShowAddSoftwareViewCommand { get; }

		public Command RemoveSoftwareCommand { get; }
	}
}
