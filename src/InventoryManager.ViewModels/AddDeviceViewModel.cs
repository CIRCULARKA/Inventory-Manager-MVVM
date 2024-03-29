using InventoryManager.Models;
using InventoryManager.Events;
using InventoryManager.Commands;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
	public class AddDeviceViewModel : ViewModelBase, IAddDeviceViewModel
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

					newDevice.CabinetID = Repository.FindCabinetByName("N/A", "N/A").ID;

					try
					{
						Repository.AddDevice(newDevice);
						Repository.SaveChanges();


						// Device should be counted as added to storage when added
						var addedDeviceNote = new DeviceMovementHistoryNote
						{
							// N/A cabinet in N/A housing
							TargetCabinetID = Repository.
								FindCabinetByName(cabName: "N/A", housingName: "N/A").ID,
							DeviceID = newDevice.ID,
							Reason = "Добавлено",
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
