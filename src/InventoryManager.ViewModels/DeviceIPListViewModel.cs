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

			RefreshAvailableIPList();

			SubscribeActionOnNetworkMaskChanges(
				RefreshAvailableIPList
			);

			SubscribeActionOnIPAssigning(
				(d) => RefreshAvailableIPList()
			);

			SubscribeActionOnIpRemoving(
				(d) => RefreshAvailableIPList()
			);

		}

		private IDeviceRelatedRepository Repository { get; }

		public event Action<IPAddress> OnIPRemoved;

		public AddIPToDeviceViewModel AddIPToDeviceViewModel =>
			ViewModelLinker.GetRegisteredViewModel<AddIPToDeviceViewModel>();

		public IPAddress SelectedIPAddress { get; set; }

		public Command AddIPToDeviceCommand { get; }

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

		public void RefreshAvailableIPList() =>
				AddIPToDeviceViewModel.
				AllAvailableIPAddresses =
				Repository.AllAvailableIPAddresses.ToList();

		private void SubscribeActionOnNetworkMaskChanges(Action action) =>
			ViewModelLinker.GetRegisteredViewModel<ConfigureIPSettingsViewModel>().
				OnNetworkMaskChanged += action;

		private void SubscribeActionOnIpRemoving(Action<IPAddress> action) =>
			this.OnIPRemoved += action;

		private void SubscribeActionOnIPAssigning(Action<IPAddress> action) =>
			AddIPToDeviceViewModel.OnIPAssigned += action;
	}
}
