using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
	public class AddIPToDeviceViewModel : ViewModelBase
	{
		private IEnumerable<IPAddress> _allAvailableIPs;

		public AddIPToDeviceViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			AddIPToDeviceCommand = RegisterCommandAction(
				(obj) => AddIPToDevice(),
				(obj) => SelectedIPAddress != null
			);
		}

		private IDeviceRelatedRepository Repository { get; }

		public event Action<IPAddress> OnIPAssigned;

		public IEnumerable<IPAddress> AllAvailableIPAddresses
		{
			get => _allAvailableIPs;
			set
			{
				_allAvailableIPs = Repository.AllAvailableIPAddresses.ToList();
				OnPropertyChanged(nameof(AllAvailableIPAddresses));
			}
		}

		public IPAddress SelectedIPAddress { get; set; }

		public Device SelectedDevice =>
			ViewModelLinker.GetRegisteredViewModel<DevicesListViewModel>().
				SelectedDevice;

		public Command AddIPToDeviceCommand { get; }

		public void AddIPToDevice()
		{
			try
			{
				Repository.AddIPToDevice(SelectedIPAddress, SelectedDevice);
				Repository.SaveChanges();

				OnIPAssigned?.Invoke(SelectedIPAddress);

				MessageToUser = "Адрес успешно добавлен";
			}
			catch (Exception m)
			{
				MessageToUser = m.Message;
			}
		}
	}
}
