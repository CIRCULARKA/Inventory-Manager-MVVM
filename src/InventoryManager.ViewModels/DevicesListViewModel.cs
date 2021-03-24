using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Views;
using InventoryManager.Infrastructure;
using InventoryManager.Extensions;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.ViewModels
{
	public class DevicesListViewModel : ViewModelBase
	{
		public DevicesListViewModel(IDeviceRelatedRepository repo, DeviceFilter filter)
		{
			Repository = repo;

			AllDevices = Repository.AllDevices.ToList();

			DevicesFilter = filter;

			FilteredDevices = DevicesFilter.
				GetFilteredDevicesList(AllDevices).
				ToObservableCollection();

			OpenAddDeviceViewCommand = RegisterCommandAction(
				(obj) => AddDeviceView.ShowDialog()

			);
			RemoveDeviceCommand = RegisterCommandAction(
				(obj) =>
				{
					Repository.RemoveDevice(SelectedDevice);

					Repository.DeleteAllDeviceMovementHistory(SelectedDevice);

					Repository.SaveChanges();
					AllDevices.Remove(AllDevices.Find(d => d.ID == SelectedDevice.ID));
					FilteredDevices.Remove(SelectedDevice);
				},
				(obj) => SelectedDevice != null
			);
		}

		private IDeviceRelatedRepository Repository { get; set; }

		public ObservableCollection<Device> FilteredDevices { get; set; }

		public DeviceFilter DevicesFilter { get; }

		public List<Device> AllDevices { get; }

		public Device SelectedDevice { get; set; }

		public Command OpenAddDeviceViewCommand { get; }

		public Command RemoveDeviceCommand { get; }

		public AddDeviceView AddDeviceView =>
			ViewModelLinker.GetRegisteredView<AddDeviceView>();
	}
}
