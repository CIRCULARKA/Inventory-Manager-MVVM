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

			try
			{
				SelectedSoftwareConfiguration = Repository.
					GetSoftwareConfiguration(SelectedSoftware);

				FillFieldsWithSoftwareConfiguration(SelectedSoftwareConfiguration);
			}
			catch (NullReferenceException) { }

			ApplyChangesCommand = RegisterCommandAction(
				(obj) =>
				{
					SoftwareConfiguration newConfig;

					try
					{
						newConfig = Repository.GetSoftwareConfiguration(
							SelectedSoftware
						);

						InitializeSoftwareConfiguration(newConfig);

						Repository.UpdateSoftwareConfiguration(newConfig);
						Repository.SaveChanges();

						MessageToUser = "Информация о ПО обновлена";
					}
					catch (NullReferenceException)
					{
						newConfig = new SoftwareConfiguration();
						newConfig.Software = SelectedSoftware;

						InitializeSoftwareConfiguration(newConfig);

						Repository.AddSoftwareConfiguration(newConfig);
						Repository.SaveChanges();

						MessageToUser = "Информация о ПО добавлена";
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

		public void FillFieldsWithSoftwareConfiguration(SoftwareConfiguration config)
		{
			Login = config.Login;
			Password = config.Password;
			AdditionalInformation = config.AdditionalInformation;
		}
	}
}
