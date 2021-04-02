using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InventoryManager.Models;
using InventoryManager.Views;
using InventoryManager.Commands;
using InventoryManager.Extensions;
using InventoryManager.Infrastructure;

namespace InventoryManager.ViewModels
{
	public class DeviceAccountsListViewModel : ViewModelBase
	{
		public DeviceAccountsListViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			SubscribeActionOnDeviceAccountAddition(
				(newAcc) => SelectedDeviceAccounts.Add(newAcc)
			);

			ShowAddDeviceAccountViewCommand = RegisterCommandAction(
				(obj) => AddDeviceAccountView.ShowDialog(),
				(obj) => SelectedDevice != null
			);

			RemoveDeviceAccountCommand = RegisterCommandAction(
				(obj) =>
				{
					RemoveAccountFromDevice(SelectedAccount);
					SelectedDeviceAccounts.Remove(SelectedAccount);
				},
				(obj) => SelectedAccount != null
			);
		}

		private IDeviceRelatedRepository Repository { get; }

		public ObservableCollection<DeviceAccount> SelectedDeviceAccounts { get; set; }

		public Device SelectedDevice =>
			ViewModelLinker.
				GetRegisteredViewModel<DevicesListViewModel>().
					SelectedDevice;

		public AddDeviceAccountView AddDeviceAccountView =>
			ViewModelLinker.GetRegisteredView<AddDeviceAccountView>();

		public DeviceAccount SelectedAccount { get; set; }

		public Command ShowAddDeviceAccountViewCommand { get; }

		public Command RemoveDeviceAccountCommand { get; }

		public void RemoveAccountFromDevice(DeviceAccount acc)
		{
			Repository.RemoveDeviceAccount(acc);
			Repository.SaveChanges();
		}

		private void SubscribeActionOnDeviceAccountAddition(Action<DeviceAccount> action) =>
			ViewModelLinker.
				GetRegisteredViewModel<AddDeviceAccountViewModel>().
					OnDeviceAccountAdded += action;
	}
}
