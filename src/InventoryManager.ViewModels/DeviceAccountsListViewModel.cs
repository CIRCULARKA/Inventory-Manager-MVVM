using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.Events;
using InventoryManager.Commands;
using InventoryManager.Extensions;
using InventoryManager.Infrastructure;
using Ninject;
using static InventoryManager.DependencyInjection.NinjectKernel;
using System;
using System.Collections.ObjectModel;

namespace InventoryManager.ViewModels
{
	public class DeviceAccountsListViewModel : ViewModelBase, IDeviceAccountsListViewModel
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
				(obj) =>
				{
					var _addDeviceAccountView = new AddDeviceAccountView();
					_addDeviceAccountView.DataContext = ResolveDependency<IAddDeviceAccountViewModel>();
					_addDeviceAccountView.ShowDialog();
				},
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

			DeviceEvents.OnDeviceSelectionChanged += device =>
				SelectedDeviceAccounts = null;
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
			(ResolveDependency<IDevicesListViewModel>() as DevicesListViewModel).
				SelectedDevice;

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
