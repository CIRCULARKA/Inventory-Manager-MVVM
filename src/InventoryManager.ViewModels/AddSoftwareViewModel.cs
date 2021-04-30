using InventoryManager.Models;
using InventoryManager.Events;
using InventoryManager.Commands;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.ViewModels
{
	public class AddSoftwareViewModel : ViewModelBase, IAddSoftwareViewModel
	{
		private string _login;

		private string _password;

		public AddSoftwareViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			try
			{
				SelectedSoftwareType = AllSoftwareTypes.First();
			}
			catch (Exception)
			{
				MessageToUser = "В базе данных нет ни одного типа программного обеспечения.\n" +
					"Попросите разработчика добавить эти типы";
				CanAdditionBeExecuted = false;
			}

			AddSoftwareCommand = RegisterCommandAction(
				(obj) =>
				{
					var newConfiguration = new SoftwareConfiguration()
					{
						Login = Login,
						Password = Password,
						AdditionalInformation = AdditionalInformation
					};

					var newSoftware = new Software()
					{
						Type = SelectedSoftwareType,
						DeviceID = SelectedDevice.ID
					};
					newConfiguration.Software = newSoftware;

					try
					{
						Repository.AddSoftware(newSoftware);
						Repository.SaveChanges();

						DeviceEvents.RaiseOnSoftwareAdded(newSoftware);

						MessageToUser = "ПО добавлено";

						Login = string.Empty;
						Password = string.Empty;
						AdditionalInformation = string.Empty;
					}
					catch (Exception e)
					{
						MessageToUser = e.Message;
						Repository.RemoveSoftware(newSoftware);
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

		public IEnumerable<SoftwareType> AllSoftwareTypes =>
			Repository.AllSoftwareTypes.ToList();

		public SoftwareType SelectedSoftwareType { get; set; }

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

		public string AdditionalInformation { get; set; }

		public bool CanAdditionBeExecuted { get; set; } = true;
	}
}
