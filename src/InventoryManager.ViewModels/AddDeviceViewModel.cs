using InventoryManager.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using InventoryManager.Commands;
using InventoryManager.Infrastructure;

namespace InventoryManager.ViewModels
{
	public class AddDeviceViewModel : ViewModelBase
	{
		private string _inputtedInventoryNumber;

		private string _inputtedNetworkName;

		public AddDeviceViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			AddDeviceCommand = RegisterCommandAction(
				(obj) =>
				{
					var newDevice = new Device
					{
						InventoryNumber = InputtedInventoryNumber,
						DeviceTypeID = SelectedDeviceType.ID,
						NetworkName = InputtedNetworkName,
					};

					try
					{
						Repository.AddDevice(newDevice);
						Repository.SaveChanges();

						// Device should be counted as added to storage when added
						var addedDeviceNote = new DeviceMovementHistoryNote
						{
							// N/A cabinet in N/A housing
							TargetCabinetID = -4,
							DeviceID = newDevice.ID,
							Reason = "Доставлено на склад",
							Date = DateTime.Now
						};
						Repository.FixDeviceMovement(addedDeviceNote);
						Repository.SaveChanges();

						DeviceEvents.RaiseOnNewDeviceAdded(newDevice);

						InputtedInventoryNumber = "";
						InputtedNetworkName = "";
						MessageToUser = "Устройство добавлено";
					}
					catch (Exception e)
					{
						MessageToUser = e.Message;
						Repository.RemoveDevice(newDevice);
					}
				},
				(obj) =>
				{
					return !string.IsNullOrEmpty(InputtedInventoryNumber) &&
						!string.IsNullOrEmpty(InputtedNetworkName) &&
						SelectedDeviceType != null;
				}
			);
		}

		private IDeviceRelatedRepository Repository { get; }

		public Command AddDeviceCommand { get; }

		public DeviceType SelectedDeviceType { get; set; }

		public List<DeviceType> AllDeviceTypes =>
			Repository.AllDeviceTypes.ToList();

		public string InputtedInventoryNumber
		{
			get => _inputtedInventoryNumber;
			set
			{
				_inputtedInventoryNumber = value;
				OnPropertyChanged("InputtedInventoryNumber");
			}
		}

		public string InputtedNetworkName
		{
			get => _inputtedNetworkName;
			set
			{
				_inputtedNetworkName = value;
				OnPropertyChanged("InputtedNetworkName");
			}
		}
	}
}
