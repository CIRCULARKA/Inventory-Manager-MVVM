using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Events;
using InventoryManager.Commands;
using InventoryManager.Extensions;
using InventoryManager.Infrastructure;
using System;
using System.Collections.ObjectModel;

namespace InventoryManager.ViewModels
{
	public class DeviceAccountsListViewModel : ViewModelBase
	{
		private ObservableCollection<DeviceAccount> _selectedDeviceAccounts;

		public DeviceAccountsListViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			SubscribeActionOnDeviceAccountAddition(
				(newAcc) => SelectedDeviceAccounts.Add(newAcc)
			);

			SubscribeActionOnDeviceSelectionChanged(
				(device) =>
				{
					if (device != null)
					{
						SelectedDeviceAccounts = Repository.
							GetAllDeviceAccounts(device).
								ToObservableCollection();
					}
				}
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

		public ObservableCollection<DeviceAccount> SelectedDeviceAccounts
		{
			get => _selectedDeviceAccounts;
			set
			{
				_selectedDeviceAccounts = value;

				OnPropertyChanged(nameof(SelectedDeviceAccounts));
			}
		}

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
			DeviceEvents.OnDeviceAccountAdded += action;

		private void SubscribeActionOnDeviceSelectionChanged(Action<Device> action) =>
			DeviceEvents.OnDeviceSelectionChanged += action;
	}
}
