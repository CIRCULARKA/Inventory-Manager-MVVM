using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;
using InventoryManager.Views;
using InventoryManager.Extensions;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;

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
				(device) => SelectedDeviceIPAddresses =
					Repository.GetAllDeviceIPAddresses(device).ToObservableCollection()
			);

			SubscribeActionOnNetworkMaskChanges(ClearDevicesIPLists);

			ShowAddIPViewCommand = RegisterCommandAction(
				(obj) =>
				{
					AddIPToDeviceView.ShowDialog();
					UIEvents.RaiseOnShowAddIPAddressViewCommandExecuted();
				},
				(obj) => SelectedDevice != null
			);

			RemoveIPFromDeviceCommand = RegisterCommandAction(
				(o) => RemoveIPAddress(SelectedIPAddress),
				(obj) => SelectedIPAddress != null
			);
		}

		private IDeviceRelatedRepository Repository { get; }

		public event Action<IPAddress> OnIPRemoved;

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
			OnIPRemoved?.Invoke(ip);
		}

		private void ClearDevicesIPLists() =>
			SelectedDeviceIPAddresses?.Clear();

		private void SubscribeActionOnIPAddition(Action<IPAddress> action) =>
			DeviceEvents.OnDeviceIPAdded += action;

		private void SubscribeActionOnNetworkMaskChanges(Action action) =>
			ViewModelLinker.GetRegisteredViewModel<ConfigureIPSettingsViewModel>()
				.OnNetworkMaskChanged += action;

		private void SubscribeActionOnDeviceSelectionChanged(Action<Device> action) =>
			DeviceEvents.OnDeviceSelectionChanged += action;
	}
}
