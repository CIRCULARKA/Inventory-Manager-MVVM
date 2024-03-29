using InventoryManager.Models;
using InventoryManager.Events;
using InventoryManager.Commands;
using InventoryManager.Networking;
using System;

namespace InventoryManager.ViewModels
{
	public class ConfigureIPSettingsViewModel : ViewModelBase
	{
		private string _inputtedNetworkAddress;

		private string _inputtedNetworkMask;

		public ConfigureIPSettingsViewModel(IIPAddressRepository repo)
		{
			NetworkConfigurator = new NetworkConfigurator(new XMLNetworkConfigurationReader());
			Repository = repo;
			InputtedNetworkMask = NetworkConfigurator.Mask.ToString();

			ApplyNetworkSettingsChangesCommand = RegisterCommandAction(
				(obj) =>
				{
					try
					{
						NetworkConfigurator.Mask = byte.Parse(InputtedNetworkMask);
						Repository.SetNewRangeOfIPAddresses(NetworkConfigurator.IPAddresses);
						Repository.SaveChanges();
						NetworkConfigurator.WriteMask(new XMLNetworkConfigurationWriter());

						MessageToUser = "Маска обновлена";

						DeviceEvents.RaiseOnNetworkConfigurationChanged(
							InputtedNetworkAddress,
							byte.Parse(InputtedNetworkMask)
						);
					}
					catch (Exception e)
					{
						MessageToUser = e.Message;
					}
				},
				(obj) =>
				{
					// Move to special validator class soon
					try
					{
						var mask = byte.Parse(InputtedNetworkMask);
						if (mask <= 32 && mask >= 0) return true;
						return false;
					}
					catch { return false; }
				}
			);
		}

		private IIPAddressRepository Repository { get; }

		public Command ApplyNetworkSettingsChangesCommand { get; }

		public string InputtedNetworkAddress
		{
			get => _inputtedNetworkAddress;
			set
			{
				_inputtedNetworkAddress = value;
				OnPropertyChanged(nameof(InputtedNetworkAddress));
			}
		}

		public string InputtedNetworkMask
		{
			get => _inputtedNetworkMask;
			set
			{
				_inputtedNetworkMask = value;
				OnPropertyChanged(nameof(InputtedNetworkMask));
			}
		}

		private INetworkConfigurator NetworkConfigurator { get; }
	}
}
