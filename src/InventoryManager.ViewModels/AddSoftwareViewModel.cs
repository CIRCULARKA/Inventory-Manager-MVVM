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
					}
					catch (Exception e)
					{
						MessageToUser = e.Message;
					}
				},
				(obj) => CanAdditionBeExecuted
			);
		}

		private IDeviceRelatedRepository Repository { get; }

		public Device SelectedDevice =>
			(ResolveDependency<IDevicesListViewModel>() as DevicesListViewModel).
				SelectedDevice;

		public Command AddSoftwareCommand { get; }

		public IEnumerable<SoftwareType> AllSoftwareTypes =>
			Repository.AllSoftwareTypes.ToList();

		public SoftwareType SelectedSoftwareType { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public string AdditionalInformation { get; set; }

		public bool CanAdditionBeExecuted { get; set; } = true;
	}
}
