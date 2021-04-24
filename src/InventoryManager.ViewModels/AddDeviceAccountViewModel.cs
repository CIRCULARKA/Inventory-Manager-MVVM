using InventoryManager.Events;
using InventoryManager.Models;
using InventoryManager.Commands;
using System;

namespace InventoryManager.ViewModels
{
	public class AddDeviceAccountViewModel : ViewModelBase, IAddDeviceAccountViewModel
	{
		private string _inputtetLogin;

		private string _inputtedPassword;

		public AddDeviceAccountViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			AddDeviceAccountCommand = RegisterCommandAction(
				(obj) => AddAccountToDevice(),
				(obj) => !(string.IsNullOrWhiteSpace(InputtedLogin) ||
					string.IsNullOrWhiteSpace(InputtedPassword))
			);
		}

		private IDeviceRelatedRepository Repository { get; }

		public Device SelectedDevice =>
			(ResolveDependency<IDevicesListViewModel>() as DevicesListViewModel).
				SelectedDevice;

		public Command AddDeviceAccountCommand { get; }

		public string InputtedLogin
		{
			get => _inputtetLogin;
			set
			{
				_inputtetLogin = value;
				OnPropertyChanged(nameof(InputtedLogin));
			}
		}

		public string InputtedPassword
		{
			get => _inputtedPassword;
			set
			{
				_inputtedPassword = value;
				OnPropertyChanged(InputtedPassword);
			}
		}

		public void AddAccountToDevice()
		{
			var newAcc = new DeviceAccount
			{
				DeviceID = SelectedDevice.ID,
				Login = InputtedLogin,
				Password = InputtedPassword
			};

			try
			{
				Repository.AddDeviceAccount(newAcc);
				Repository.SaveChanges();

				DeviceEvents.RaiseOnDeviceAccountAdded(newAcc);

				InputtedLogin = "";
				InputtedPassword = "";

				MessageToUser = "Учётная запись успешно добавлена";
			}
			catch (Exception)
			{
				MessageToUser = "Учётная запись с таким логином уже существует";
				Repository.RemoveDeviceAccount(newAcc);
			}
		}

	}
}
