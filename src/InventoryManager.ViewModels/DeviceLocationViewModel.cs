using InventoryManager.Models;
using InventoryManager.Events;
using InventoryManager.Commands;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
	public class DeviceLocationViewModel : ViewModelBase, IDeviceLocationViewModel
	{
		private bool _isDeviceLocationChoosingAvailable;

		private Housing _selectedHousing;

		private Cabinet _selectedCabinet;

		private IEnumerable<Cabinet> _selectedHousingCabinets;

		public DeviceLocationViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			AllHousings = Repository.AllHousings.ToList();
			AllCabinets = Repository.AllCabinets.ToList();

			IsDeviceLocationChoosingAvailable = false;

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
					catch (Exception e) { MessageToUser = e.Message; }
				},
				(obj) => (SelectedDevice != null) &&
					(SelectedDevice.Cabinet != SelectedCabinet)
			);

			SubscribeActionOnHousingChanged(
				(h) =>
				{
					if (h != null)
					{
						SelectedHousingCabinets = GetAllSelectedHousingCabinets();
						SelectDeviceCabinetIfHousingIsTheSame();
					}
					else SelectedHousingCabinets = null;
				}
			);

			SubscribeActionOnDeviceSelectionChanging(
				(device) =>
				{
					if (device != null)
					{
						SelectedCabinet = device?.Cabinet;
						SelectedHousing = device?.Cabinet?.Housing;
					}
				}
			);

			DeviceEvents.OnDeviceSelectionChanged += device =>
			{
				if (device != null)
					IsDeviceLocationChoosingAvailable = true;
				else
				{
					SelectedCabinet = null;
					SelectedHousing = null;

					IsDeviceLocationChoosingAvailable = false;
				}
			};
		}

		private IDeviceRelatedRepository Repository { get; }

		public IEnumerable<Housing> AllHousings { get; }

		public IEnumerable<Cabinet> AllCabinets { get; }

		public IEnumerable<Cabinet> SelectedHousingCabinets
		{
			get => _selectedHousingCabinets;
			set
			{
				_selectedHousingCabinets = value;

				OnPropertyChanged(nameof(SelectedHousingCabinets));
			}
		}

		public Cabinet SelectedCabinet
		{
			get => _selectedCabinet;
			set
			{
				_selectedCabinet = value;
				OnPropertyChanged(nameof(SelectedCabinet));
			}
		}

		public Housing SelectedHousing
		{
			get => _selectedHousing;
			set
			{
				_selectedHousing = value;

				DeviceEvents.RaiseOnDeviceHousingChanged(_selectedHousing);
				OnPropertyChanged(nameof(SelectedHousing));
			}
		}

		private Device SelectedDevice =>
			(ResolveDependency<IDevicesListViewModel>() as DevicesListViewModel).
				SelectedDevice;

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

		public IEnumerable<Cabinet> GetAllSelectedHousingCabinets() =>
			AllCabinets.
				Where(c => c.HousingID == SelectedHousing.ID).
				ToList();

		public void SelectDeviceCabinetIfHousingIsTheSame()
		{
			if (SelectedDevice.Cabinet.HousingID == SelectedHousing.ID)
				SelectedCabinet = SelectedDevice?.Cabinet;
			else
				SelectedCabinet = AllCabinets.First(c => c.HousingID == SelectedHousing.ID);
		}

		public void SubscribeActionOnHousingChanged(Action<Housing> action) =>
			DeviceEvents.OnDeviceHosuingChanged += action;

		public void SubscribeActionOnDeviceSelectionChanging(Action<Device> action) =>
			DeviceEvents.OnDeviceSelectionChanged += action;
	}
}
