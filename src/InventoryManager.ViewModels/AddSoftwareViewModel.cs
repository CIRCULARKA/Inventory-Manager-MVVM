using InventoryManager.Models;
using InventoryManager.Events;
using InventoryManager.Commands;
using InventoryManager.Extensions;
using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace InventoryManager.ViewModels
{
	public class AddSoftwareViewModel : ViewModelBase, IAddSoftwareViewModel
	{
		private string _login;

		private string _password;

		private string _additionalInformation;

		private Software _newSoftware;

		private SoftwareType _selectedSoftwareType;

		public AddSoftwareViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			AvailableSoftwareTypes = Repository.AllSoftwareTypes.Where(
				st =>
				{
					foreach (var software in Repository.GetAllDeviceSoftware(SelectedDevice))
						if (st.ID == software.Type.ID) return false;
					return true;
				}
			).ToObservableCollection();

			try { SelectedSoftwareType = AvailableSoftwareTypes.First(); }
			catch (Exception)
			{
				MessageToUser = "Выбранное устройство уже имеет всё ПО, зарегестрированное в базе";
				CanAdditionBeExecuted = false;
			}

			AddSoftwareCommand = RegisterCommandAction(
				(obj) =>
				{

					try
					{
						AddSoftwareToDevice();
						RemoveChosenSoftwareTypeFromList();
						PickFirstSoftwareTypeInList();

						MessageToUser = "ПО добавлено";

						Login = string.Empty;
						Password = string.Empty;
						AdditionalInformation = string.Empty;
					}
					catch
					{
						MessageToUser = "Указанное ПО на выбранном устройстве уже установлено";
						Repository.RemoveSoftware(_newSoftware);
					}
				},
				(obj) => SelectedSoftwareType != null && CanAdditionBeExecuted
			);

			DeviceEvents.OnSoftwareAdded += (software) =>
			{

			};
		}

		private IDeviceRelatedRepository Repository { get; }

		public Device SelectedDevice =>
			(ResolveDependency<IDevicesListViewModel>() as DevicesListViewModel).
				SelectedDevice;

		public Command AddSoftwareCommand { get; }

		public ObservableCollection<SoftwareType> AvailableSoftwareTypes { get; }

		public SoftwareType SelectedSoftwareType
		{
			get => _selectedSoftwareType;
			set
			{
				_selectedSoftwareType = value;
				OnPropertyChanged(nameof(SelectedSoftwareType));
			}
		}

		public bool IsConfigurationEmpty =>
			string.IsNullOrWhiteSpace(Login) && string.IsNullOrWhiteSpace(Password) &&
			string.IsNullOrWhiteSpace(AdditionalInformation);

		public string Login
		{
			get => _login;
			set
			{
				_login = value;
				OnPropertyChanged(nameof(Login));
			}
		}

		public string Password
		{
			get => _password;
			set
			{
				_password = value;
				OnPropertyChanged(nameof(Password));
			}
		}

		public string AdditionalInformation
		{
			get => _additionalInformation;
			set
			{
				_additionalInformation = value;
				OnPropertyChanged(nameof(AdditionalInformation));
			}
		}

		public bool CanAdditionBeExecuted { get; set; } = true;

		public void AddSoftwareToDevice()
		{
			var newConfiguration = new SoftwareConfiguration()
			{
				Login = Login,
				Password = Password,
				AdditionalInformation = AdditionalInformation
			};

			Repository.AddSoftwareConfiguration(newConfiguration);
			Repository.SaveChanges();

			_newSoftware = new Software()
			{
				Type = SelectedSoftwareType,
				DeviceID = SelectedDevice.ID,
				Configuration = newConfiguration
			};

			Repository.AddSoftware(_newSoftware);
			Repository.SaveChanges();

			DeviceEvents.RaiseOnSoftwareAdded(_newSoftware);
		}

		public void RemoveChosenSoftwareTypeFromList() =>
			AvailableSoftwareTypes.Remove(SelectedSoftwareType);

		public void PickFirstSoftwareTypeInList()
		{
			if (AvailableSoftwareTypes.Count > 0)
				SelectedSoftwareType = AvailableSoftwareTypes.First();
		}
	}
}
