using InventoryManager.Models;
using InventoryManager.Commands;
using System;

namespace InventoryManager.ViewModels
{
	public class AddIPAddressViewModel : ViewModelBase
	{
		private string _inputtedIPAddress;

		public AddIPAddressViewModel(IIPAddressRepository repo)
		{
			Repository = repo;

			AddIPToDeviceCommand = RegisterCommandAction(
				(obj) =>
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
	}
}
