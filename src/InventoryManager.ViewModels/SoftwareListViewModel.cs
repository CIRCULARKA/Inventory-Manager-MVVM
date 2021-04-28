using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Events;
using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace InventoryManager.ViewModels
{
	public class SoftwareListViewModel : ViewModelBase, ISoftwareListViewModel
	{
		public SoftwareListViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			ShowAddSoftwareViewCommand = RegisterCommandAction(
				(obj) => MessageBox.Show(
					"Not yet implemented",
					"Will be implemented soon",
					MessageBoxButton.OK,
					MessageBoxImage.Exclamation
				),
				(obj) => SelectedSoftware != null
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
						(ResolveDependency<IDevicesListViewModel>() as DevicesListViewModel).
							SelectedDevice
					).ToList();
				}
			};
		}

		private IDeviceRelatedRepository Repository { get; }

		public IEnumerable<Software> SelectedDeviceSoftware { get; set; }

		public Software SelectedSoftware { get; set; }

		public Command ShowAddSoftwareViewCommand { get; }

		public Command RemoveSoftwareCommand { get; }
	}
}
