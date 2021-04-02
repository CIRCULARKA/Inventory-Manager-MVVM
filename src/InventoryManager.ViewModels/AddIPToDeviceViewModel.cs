using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
	public class AddIPToDeviceViewModel : ViewModelBase
	{
		private IEnumerable<IPAddress> _allAvailableIPs;

		public AddIPToDeviceViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			AddIPToDeviceCommand = RegisterCommandAction(
				(obj) => AddIPToDevice(),
				(obj) => SelectedIPAddress != null
			);

			SubscribeActionOnIPAssigning(
				(d) => RefreshAvailableIPList()
			);

			SubscribeActionOnNetworkMaskChanges(
				RefreshAvailableIPList
			);

			// SubscribeActionOnIpRemoving(
			// 	(d) => RefreshAvailableIPList()
			// );
		}

		private IDeviceRelatedRepository Repository { get; }

		public IEnumerable<IPAddress> AllAvailableIPAddresses
		{
			get => _allAvailableIPs;
			set
			{
				_allAvailableIPs = Repository.AllAvailableIPAddresses.ToList();
				OnPropertyChanged(nameof(AllAvailableIPAddresses));
			}
		}

		public IPAddress SelectedIPAddress { get; set; }

		public Device SelectedDevice =>
			ViewModelLinker.
				GetRegisteredViewModel<DevicesListViewModel>().
					SelectedDevice;

		public Command AddIPToDeviceCommand { get; }

		public void AddIPToDevice()
		{
			try
			{
				Repository.AddIPToDevice(SelectedIPAddress, SelectedDevice);
				Repository.SaveChanges();

				DeviceEvents.RaiseOnDeviceIPAdded(SelectedIPAddress);

				MessageToUser = "Адрес успешно добавлен";
			}
			catch (Exception m)
			{
				MessageToUser = m.Message;
			}
		}

		public void RefreshAvailableIPList() =>
			AllAvailableIPAddresses =
				Repository.
					AllAvailableIPAddresses.
						ToList();

		private void SubscribeActionOnIPAssigning(Action<IPAddress> action) =>
			DeviceEvents.OnDeviceIPAdded += action;

		// private void SubscribeActionOnIpRemoving(Action<IPAddress> action) =>
		// 	ViewModelLinker.
		// 		GetRegisteredViewModel<DeviceIPListViewModel>().
		// 			OnIPRemoved += action;

		private void SubscribeActionOnNetworkMaskChanges(Action action) =>
			ViewModelLinker.
				GetRegisteredViewModel<ConfigureIPSettingsViewModel>().
					OnNetworkMaskChanged += action;
	}
}
