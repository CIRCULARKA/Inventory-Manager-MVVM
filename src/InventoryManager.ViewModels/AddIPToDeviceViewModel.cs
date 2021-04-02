using InventoryManager.Models;
using InventoryManager.Events;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
	public class AddIPToDeviceViewModel : ViewModelBase
	{
		private List<IPAddress> _allAvailableIPs;

		private IPAddress _selectedIP;

		public AddIPToDeviceViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			AddIPToDeviceCommand = RegisterCommandAction(
				(obj) =>
				{
					AddIPToDevice();
					SelectFirstIPInList();
				},
				(obj) => SelectedIPAddress != null
			);

			SubscribeActionOnIPAssigning(
				(d) => RefreshAvailableIPList()
			);

			SubscribeActionOnNetworkMaskChanges(
				RefreshAvailableIPList
			);

			SubscribeActionOnShowAddIPAddressViewCommandExecution(
				() =>
				{
					AllAvailableIPAddresses =
						Repository.
							AllAvailableIPAddresses.
								ToList();
					SelectFirstIPInList();
				}
			);
		}

		private IDeviceRelatedRepository Repository { get; }

		public List<IPAddress> AllAvailableIPAddresses
		{
			get => _allAvailableIPs;
			set
			{
				_allAvailableIPs = value;
				OnPropertyChanged(nameof(AllAvailableIPAddresses));
			}
		}

		public IPAddress SelectedIPAddress
		{
			get => _selectedIP;
			set
			{
				_selectedIP = value;
				OnPropertyChanged(nameof(SelectedIPAddress));
			}
		}

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

		private void SelectFirstIPInList()
		{
			if (AllAvailableIPAddresses.Count > 0)
				SelectedIPAddress = AllAvailableIPAddresses.First();
		}

		public void RefreshAvailableIPList() =>
			AllAvailableIPAddresses =
				Repository.
					AllAvailableIPAddresses.
						ToList();

		private void SubscribeActionOnIPAssigning(Action<IPAddress> action) =>
			DeviceEvents.OnDeviceIPAdded += action;

		private void SubscribeActionOnNetworkMaskChanges(Action action) =>
			ViewModelLinker.
				GetRegisteredViewModel<ConfigureIPSettingsViewModel>().
					OnNetworkMaskChanged += action;

		private void SubscribeActionOnShowAddIPAddressViewCommandExecution(Action action) =>
			UIEvents.OnShowAddIPAddressViewCommandExecuted += action;
	}
}
