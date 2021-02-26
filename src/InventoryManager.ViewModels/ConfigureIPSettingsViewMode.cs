using InventoryManager.Commands;
using InventoryManager.Views;
using System.Windows;
using System.Collections;

namespace InventoryManager.ViewModels
{
	public class ConfigureIPSettingsViewModel : ViewModelBase
	{
		private string _inputtedNetworkAddress;

		private string _inputtedNetworkMask;

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

	}
}
