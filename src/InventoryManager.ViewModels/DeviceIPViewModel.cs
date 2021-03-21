using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
	public class DeviceIPViewModel : ViewModelBase
	{
		private string _inputtedIPAddress;

		private IEnumerable<IPAddress> _allAvailableIPs;

		public DeviceIPViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			RefreshAvailableIPList();

			SubscribeActionOnNetworkConfigurationChanges(
				RefreshAvailableIPList
			);

			SubscribeActionOnIPAssigning(
				(d) => RefreshAvailableIPList()
			);

			SubscribeActionOnIpRemoving(
				(d) => RefreshAvailableIPList()
			);

			AddIPToDeviceCommand = RegisterCommandAction(
				(obj) => AddIPToDevice()
			);
		}

		private IDeviceRelatedRepository Repository { get; }

		public event Action<IPAddress> OnIPAssigned;

		public event Action<IPAddress> OnIPRemoved;

		public IEnumerable<IPAddress> AllAvailableIPAddresses
		{
			get => _allAvailableIPs;
			set
			{
				_allAvailableIPs = Repository.AllAvailableIPAddresses.ToList();
				OnPropertyChanged(nameof(AllAvailableIPAddresses));
			}
		}

		public Device TargetDevice { get; set; }

		public IPAddress SelectedIPAddress { get; set; }

		public Command AddIPToDeviceCommand { get; }

		public string InputtedIPAddress
		{
			get => _inputtedIPAddress;
			set
			{
				_inputtedIPAddress = value;
				OnPropertyChanged(nameof(InputtedIPAddress));
			}
		}

		public void AddIPToDevice()
		{
			try
			{
				Repository.AddIPToDevice(SelectedIPAddress, TargetDevice);
				Repository.SaveChanges();

				OnIPAssigned?.Invoke(SelectedIPAddress);

				InputtedIPAddress = "";
				MessageToUser = "Адрес успешно добавлен";
			}
			catch (Exception m)
			{
				MessageToUser = m.Message;
			}
		}

		public void RemoveIPAddress(IPAddress ip)
		{
			Repository.RemoveIPFromDevice(ip, TargetDevice);
			Repository.SaveChanges();
			OnIPRemoved?.Invoke(ip);
		}

		public void RefreshAvailableIPList() =>
			AllAvailableIPAddresses =
				Repository.AllAvailableIPAddresses.ToList();

		private void SubscribeActionOnNetworkConfigurationChanges(Action action) =>
			ViewModelLinker.GetRegisteredViewModel<ConfigureIPSettingsViewModel>().
				OnNetworkMaskChanged += action;

		private void SubscribeActionOnIpRemoving(Action<IPAddress> action) =>
			this.OnIPRemoved += action;

		private void SubscribeActionOnIPAssigning(Action<IPAddress> action) =>
			this.OnIPAssigned += action;
	}
}
