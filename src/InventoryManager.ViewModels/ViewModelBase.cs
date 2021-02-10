using InventoryManager.Models;

namespace InventoryManager.ViewModels
{
	public class ViewModelBase : NotifyingModel
	{
		private string _messageToUser;

		private Device _deviceModel;

		private DeviceType _deviceTypeModel;

		private User _userModel;

		private Group _groupModel;

		private Housing _housingModel;

		private Cabinet _cabinetModel;

		private Certificate _certificateModel;

		private IPAddress _ipAddressModel;

		private Account _accountModel;

		private DeviceCabinet _deviceCabinet;

		public ViewModelBase()
		{
			_deviceModel = new Device();
			_deviceTypeModel = new DeviceType();
			_userModel = new User();
			_groupModel = new Group();
			_housingModel = new Housing();
			_cabinetModel = new Cabinet();
			_certificateModel = new Certificate();
			_ipAddressModel = new IPAddress();
			_accountModel = new Account();
			_deviceCabinet = new DeviceCabinet();
		}

		public string MessageToUser
		{
			get => _messageToUser;
			set
			{
				_messageToUser = value;
				OnPropertyChanged("MessageToUser");
			}
		}

		public Device DeviceModel => _deviceModel;

		public Account AccountModel => _accountModel;

		public Cabinet CabinetModel => _cabinetModel;

		public Certificate CertificateModel => _certificateModel;

		public DeviceType DeviceTypeModel => _deviceTypeModel;

		public Group GroupModel => _groupModel;

		public Housing HousingModel => _housingModel;

		public IPAddress IPAddressModel => _ipAddressModel;

		public User UserModel => _userModel;

		public DeviceCabinet DeviceCabinetModel => _deviceCabinet;
	}
}
