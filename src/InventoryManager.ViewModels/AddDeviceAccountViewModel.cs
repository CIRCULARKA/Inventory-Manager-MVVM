using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Extensions;
using InventoryManager.Infrastructure;

namespace InventoryManager.ViewModels
{
	public class AddDeviceAccountViewModel : ViewModelBase
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

		public event Action<DeviceAccount> OnDeviceAccountAdded;

		public Device SelectedDevice =>
			ViewModelLinker.
				GetRegisteredViewModel<DevicesListViewModel>().
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

				OnDeviceAccountAdded?.Invoke(newAcc);

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
