using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;
using InventoryManager.Extensions;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
	public class DeviceIPListViewModel : ViewModelBase
	{
		public DeviceIPListViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			SelectedDeviceIPAddresses = Repository.GetAllDeviceIPAddresses(SelectedDevice).
				ToObservableCollection();

			SubscribeActionOnIPAddition(
				(ipAddress) => SelectedDeviceIPAddresses.Add(ipAddress)
			);

			SubscribeActionOnNetworkMaskChanges(ClearDevicesIPLists);

			RemoveIPFromDeviceCommand = RegisterCommandAction(
				(o) => RemoveIPAddress(SelectedIPAddress)
			);
		}

		private IDeviceRelatedRepository Repository { get; }

		public event Action<IPAddress> OnIPRemoved;

		public AddIPToDeviceViewModel AddIPToDeviceViewModel =>
			ViewModelLinker.GetRegisteredViewModel<AddIPToDeviceViewModel>();

		public IPAddress SelectedIPAddress { get; set; }

		public ObservableCollection<IPAddress> SelectedDeviceIPAddresses { get; set; }

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

		private void ClearDevicesIPLists() =>
			SelectedDeviceIPAddresses?.Clear();

		private void SubscribeActionOnIPAddition(Action<IPAddress> action) =>
			ViewModelLinker.
				GetRegisteredViewModel<AddIPToDeviceViewModel>().
					OnIPAssigned += action;

		private void SubscribeActionOnNetworkMaskChanges(Action action) =>
			ViewModelLinker.GetRegisteredViewModel<ConfigureIPSettingsViewModel>()
				.OnNetworkMaskChanged += action;
	}
}
