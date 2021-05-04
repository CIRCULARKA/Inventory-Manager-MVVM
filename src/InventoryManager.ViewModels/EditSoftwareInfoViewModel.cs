using InventoryManager.Models;

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
		}

		private IDeviceRelatedRepository Repository { get; }

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
