using InventoryManager.Models;
using InventoryManager.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.ViewModels
{
	public class DeviceIPViewModel : ViewModelBase
	{
		private string _inputtedIPAddress;

		public DeviceIPViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			AddIPToDeviceCommand = RegisterCommandAction(
				(obj) => AddIPToDevice()
			);
		}

		private IDeviceRelatedRepository Repository { get; }

		public event Action<IPAddress> OnIPAdded;

		public IEnumerable<IPAddress> AllAvailableIPAddresses =>
			Repository.AllAvailableIPAddresses.ToList();

		public Device TargetDevice { get; set; }

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
			var newIP = new IPAddress
			{
				Address = InputtedIPAddress,
				DeviceID = TargetDevice.ID
			};

			try
			{
				Repository.AddIPToDevice(newIP, TargetDevice);
				Repository.SaveChanges();

				OnIPAdded?.Invoke(newIP);

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
		}
	}
}
