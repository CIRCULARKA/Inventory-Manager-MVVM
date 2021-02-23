using System;
using InventoryManager.Models;
using InventoryManager.Commands;

namespace InventoryManager.ViewModels
{
	public class AddDeviceAccountViewModel : ViewModelBase
	{
		private string _inputtedDeviceAccountName;

		private string _inputtedDevicePassword;

		public AddDeviceAccountViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			AddDeviceAccountCommand = RegisterCommandAction(
				(obj) =>
				{
					var newAcc = new DeviceAccount
					{
						DeviceID = TargetDevice.ID,
						Login = InputtedDeviceAccountLogin,
						Password = InputtedDeviceAccountPassword
					};

					try
					{
						Repository.AddDeviceAccount(newAcc);
						Repository.SaveChanges();

						OnDeviceAccountAdded?.Invoke(newAcc);

						InputtedDeviceAccountLogin = "";
						InputtedDeviceAccountPassword = "";

						MessageToUser = "Учётная запись успешно добавлена";
					}
					catch (Exception)
					{
						MessageToUser = "Учётная запись с таким логином уже существует";
						Repository.RemoveDeviceAccount(newAcc);
					}
				},
				(obj) => !(string.IsNullOrWhiteSpace(InputtedDeviceAccountLogin) ||
					string.IsNullOrWhiteSpace(InputtedDeviceAccountPassword))
			);
		}

		private IDeviceRelatedRepository Repository { get; }

		public event Action<DeviceAccount> OnDeviceAccountAdded;

		public Device TargetDevice { get; set; }

		public ButtonCommand AddDeviceAccountCommand { get; }

		public string InputtedDeviceAccountLogin
		{
			get => _inputtedDeviceAccountName;
			set
			{
				_inputtedDeviceAccountName = value;
				OnPropertyChanged("InputtedDeviceDeviceAccountName");
			}
		}

		public string InputtedDeviceAccountPassword
		{
			get => _inputtedDevicePassword;
			set
			{
				_inputtedDevicePassword = value;
				OnPropertyChanged("InputtedDevicePassword");
			}
		}
	}
}
