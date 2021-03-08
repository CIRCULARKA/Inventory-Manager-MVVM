using InventoryManager.Commands;
using InventoryManager.Infrastructure;
using InventoryManager.Models;
using InventoryManager.Views;
using System.Windows;
using System;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
	public class ConfigureIPSettingsViewModel : ViewModelBase
	{
		private string _inputtedNetworkAddress;

		private string _inputtedNetworkMask;

		public ConfigureIPSettingsViewModel(IIPAddressRepository repo)
		{
			NetworkConfigurator = new NetworkConfigurator();
			Repository = repo;

			ApplyNetworkSettingsChangesCommand = RegisterCommandAction(
				(obj) =>
				{
					try
					{
						NetworkConfigurator.Mask = byte.Parse(InputtedNetworkMask);
						Repository.SetNewRangeOfIPAddresses(NetworkConfigurator.IPAddresses);
						Repository.SaveChanges();
					}
					catch (Exception e)
					{
						MessageToUser = e.Message;
					}
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
