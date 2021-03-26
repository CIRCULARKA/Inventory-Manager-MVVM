using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
	public class DeviceIPListViewModel : ViewModelBase
	{
		public DeviceIPListViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			RemoveIPFromDeviceCommand = RegisterCommandAction(
				(o) =>
				{
					Repository.RemoveIPFromDevice(SelectedIPAddress, SelectedDevice);
					Repository.SaveChanges();

					OnIPRemoved?.Invoke(SelectedIPAddress);
				}
			);
		}

		private IDeviceRelatedRepository Repository { get; }

		public event Action<IPAddress> OnIPRemoved;

		public AddIPToDeviceViewModel AddIPToDeviceViewModel =>
			ViewModelLinker.GetRegisteredViewModel<AddIPToDeviceViewModel>();

		public IPAddress SelectedIPAddress { get; set; }

		public Device SelectedDevice =>
			ViewModelLinker.
				GetRegisteredViewModel<DevicesListViewModel>().
					SelectedDevice;

		public Command RemoveIPFromDeviceCommand { get; }

		public void RemoveIPAddress(IPAddress ip)
		{
			Repository.RemoveIPFromDevice(
				ip,
				ViewModelLinker.
					GetRegisteredViewModel<DevicesListViewModel>().
					SelectedDevice
			);
			Repository.SaveChanges();
			OnIPRemoved?.Invoke(ip);
		}
	}
}
