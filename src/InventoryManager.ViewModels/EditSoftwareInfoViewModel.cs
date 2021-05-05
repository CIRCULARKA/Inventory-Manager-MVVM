using InventoryManager.Models;
using InventoryManager.Commands;
using System;
using System.Linq;

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

			SelectedSoftwareConfiguration = Repository.
				GetSoftwareConfiguration(SelectedSoftware);

			if (SelectedSoftwareConfiguration != null)
			{
				Login = SelectedSoftwareConfiguration.Login;
				Password = SelectedSoftwareConfiguration.Password;
				AdditionalInformation = SelectedSoftwareConfiguration.AdditionalInformation;
			}

			ApplyChangesCommand = RegisterCommandAction(
				(obj) =>
				{
					try
					{
						var newConfig = Repository.GetSoftwareConfiguration(
							SelectedSoftware
						);

						if (newConfig == null)
							newConfig = new SoftwareConfiguration();

						newConfig.Login = Login;
						newConfig.Password = Password;
						newConfig.AdditionalInformation = AdditionalInformation;

						try { Repository.UpdateSoftwareConfiguration(newConfig); }
						catch { Repository.AddSoftwareConfiguration(newConfig); }
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
	}
}
