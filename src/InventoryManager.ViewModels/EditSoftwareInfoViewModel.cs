using InventoryManager.Models;
using InventoryManager.Commands;
using System;

namespace InventoryManager.ViewModels
{
	public class EditSoftwareInfoViewModel : ViewModelBase, IEditSoftwareInfoViewModel
	{
		private string _login;

		private string _password;

		private string _addInfo;

		public EditSoftwareInfoViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;

			FillFieldsWithSoftwareConfiguration();

			ApplyChangesCommand = RegisterCommandAction(
				(obj) =>
				{
					var newConfig = new SoftwareConfiguration();

					try
					{
						InitializeSoftwareConfiguration(newConfig);

						Repository.UpdateSoftwareConfiguration(newConfig);
						Repository.SaveChanges();

						MessageToUser = "Информация о ПО обновлена";
					}
					catch (Exception e) { MessageToUser = e.Message; }
				}
			);
		}

		private IDeviceRelatedRepository Repository { get; }

		public Software SelectedSoftware =>
			(ResolveDependency<ISoftwareListViewModel>() as SoftwareListViewModel).
				SelectedSoftware;

		public SoftwareConfiguration SelectedSoftwareConfiguration { get; set; }

		public Command ApplyChangesCommand { get; }

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
			get => _addInfo;
			set
			{
				_addInfo = value;
				OnPropertyChanged(nameof(AdditionalInformation));
			}
		}

		public void InitializeSoftwareConfiguration(SoftwareConfiguration config)
		{
			config.Login = Login;
			config.Password = Password;
			config.AdditionalInformation = AdditionalInformation;
		}

		public void FillFieldsWithSoftwareConfiguration()
		{
			var config = SelectedSoftware.Configuration;
			Login = config.Login;
			Password = config.Password;
			AdditionalInformation = config.AdditionalInformation;
		}
	}
}
