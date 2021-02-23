using InventoryManager.Models;
using InventoryManager.Commands;
using System;

namespace InventoryManager.ViewModels
{
	public class DeviceIPViewModel : ViewModelBase
	{
		private string _inputtedIPAddress;

		public DeviceIPViewModel(IIPAddressRepository repo)
		{
			Repository = repo;

			AddIPToDeviceCommand = RegisterCommandAction(
				(obj) => AddIPToDevice()
			);
		}

		private IIPAddressRepository Repository { get; }

		public event Action<IPAddress> OnIPAdded;

		public Device DeviceToAddIPTo { get; set; }

		public ButtonCommand AddIPToDeviceCommand { get; }

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
				DeviceID = DeviceToAddIPTo.ID
			};

			try
			{
				Repository.AddIPAddress(newIP);
				Repository.SaveChanges();

				OnIPAdded?.Invoke(newIP);

				InputtedIPAddress = "";
				MessageToUser = "Адрес успешно добавлен";
			}
			catch (Exception)
			{
				MessageToUser = "Такой адрес уже используется";
				Repository.RemoveIPAddress(newIP);
			}
		}

		public void RemoveIPAddress(IPAddress ip)
		{
			Repository.RemoveIPAddress(ip);
			Repository.SaveChanges();
		}
	}
}
