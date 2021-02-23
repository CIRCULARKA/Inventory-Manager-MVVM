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
				(obj) => AddAccountToDevice(),
				(obj) => !(string.IsNullOrWhiteSpace(InputtedDeviceAccountLogin) ||
					string.IsNullOrWhiteSpace(InputtedDeviceAccountPassword))
			);
		}

		private IDeviceRelatedRepository Repository { get; }

		public event Action<DeviceAccount> OnDeviceAccountAdded;

		public event Action<DeviceAccount> OnDeviceAccountRemoved;

		public Device TargetDevice { get; set; }

		public ButtonCommand AddDeviceAccountCommand { get; }

		public ButtonCommand RemoveDeviceAccountCommand { get; }

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

		public void AddAccountToDevice()
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
		}

		// public void RemoveAccountFromDevice()
		// {
		// 	Repository.RemoveDeviceAccount(SelectedDeviceAccount);
		// 	Repository.SaveChanges();

		// 	SelectedDeviceAccounts.Remove(SelectedDeviceAccount);
		// }
	}
}
