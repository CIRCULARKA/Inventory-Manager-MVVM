using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Events;
using InventoryManager.Commands;
using InventoryManager.Extensions;
using InventoryManager.Infrastructure;
using System;
using System.Collections.ObjectModel;

namespace InventoryManager.ViewModels
{
	public class DeviceIPListViewModel : ViewModelBase
	{
		private ObservableCollection<IPAddress> _selectedDeviceIPAddresses;

		public DeviceIPListViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			SubscribeActionOnIPAddition(
				(ipAddress) => SelectedDeviceIPAddresses.Add(ipAddress)
			);

			SubscribeActionOnDeviceSelectionChanged(
				(device) =>
				{
					if (device != null)
						SelectedDeviceIPAddresses =
							Repository.GetAllDeviceIPAddresses(device).ToObservableCollection();
				}
			);

			SubscribeActionOnNetworkMaskChanges((ip, mask) => ClearDevicesIPLists());

			ShowAddIPViewCommand = RegisterCommandAction(
				(obj) =>
				{
					UIEvents.RaiseOnShowAddIPAddressViewCommandExecuted();
					AddIPToDeviceView.ShowDialog();
				},
				(obj) => SelectedDevice != null
			);

			RemoveIPFromDeviceCommand = RegisterCommandAction(
				(o) =>
				{
					RemoveIPAddress(SelectedIPAddress);
					SelectedDeviceIPAddresses.Remove(SelectedIPAddress);
				},
				(obj) => SelectedIPAddress != null
			);
		}

		private IDeviceRelatedRepository Repository { get; }

		public AddIPAddressView AddIPToDeviceView =>
			ViewModelLinker.GetRegisteredView<AddIPAddressView>();

		public IPAddress SelectedIPAddress { get; set; }

		public ObservableCollection<IPAddress> SelectedDeviceIPAddresses
		{
			get => _selectedDeviceIPAddresses;
			set
			{
				_selectedDeviceIPAddresses = value;
				OnPropertyChanged(nameof(SelectedDeviceIPAddresses));
			}
		}

		public Device SelectedDevice =>
			ViewModelLinker.
				GetRegisteredViewModel<DevicesListViewModel>().
					SelectedDevice;

		public Command ShowAddIPViewCommand { get; }

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
			DeviceEvents.RaiseOnDeviceIPRemoved(ip);
		}

		private void ClearDevicesIPLists() =>
			SelectedDeviceIPAddresses?.Clear();

		private void SubscribeActionOnIPAddition(Action<IPAddress> action) =>
			DeviceEvents.OnDeviceIPAdded += action;

		private void SubscribeActionOnNetworkMaskChanges(Action<string, byte> action) =>
			DeviceEvents.OnNetworkConfigurationChanged += action;

		private void SubscribeActionOnDeviceSelectionChanged(Action<Device> action) =>
			DeviceEvents.OnDeviceSelectionChanged += action;
	}
}
