using InventoryManager.Models;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
	public class DeviceLocationViewModel : ViewModelBase
	{
		private bool _isDeviceLocationChoosingAvailable;

		public DeviceLocationViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			ApplyDeviceLocationChangesCommand = RegisterCommandAction(
				(obj) =>
				{
					SelectedDevice.Cabinet = SelectedCabinet;
					SelectedDevice.Cabinet.Housing = SelectedHousing;

					try
					{
						Repository.UpdateDevice(SelectedDevice);

						var newRecord = new DeviceMovementHistoryNote()
						{
							DeviceID = SelectedDevice.ID,
							TargetCabinetID = SelectedDevice.Cabinet.ID,
							Date = DateTime.Now,
							// Reason field is temporary. Need to create entity with reasons
							// and use it instead of this hard coding
							Reason = "Перемещение"
						};

						try
						{
							Repository.FixDeviceMovement(newRecord);
							Repository.SaveChanges();
						}
						catch
						{
							Repository.RemoveDeviceMovementNote(newRecord);
						}

						Repository.SaveChanges();
					}
					catch (Exception e)
					{
						MessageToUser = e.Message;
					}
				},
				(obj) => SelectedDevice != null
			);

		}

		public Cabinet SelectedCabinet { get; set; }

		public Housing SelectedHousing { get; set; }

		private Device SelectedDevice =>
			ViewModelLinker.GetRegisteredViewModel<DevicesListViewModel>().SelectedDevice;

		private IDeviceRelatedRepository Repository { get; }

		public Command ApplyDeviceLocationChangesCommand { get; }

		public bool IsDeviceLocationChoosingAvailable
		{
			get => _isDeviceLocationChoosingAvailable;
			set
			{
				_isDeviceLocationChoosingAvailable = value;
				OnPropertyChanged(nameof(IsDeviceLocationChoosingAvailable));
			}
		}
	}
}
