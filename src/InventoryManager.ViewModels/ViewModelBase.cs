using InventoryManager.Models;

namespace InventoryManager.ViewModels
{
	public class ViewModelBase : NotifyingModel
	{
		private string _messageToUser;

		private Device _deviceModel = new Device();

		private DeviceType _deviceTypeModel = new DeviceType();

		private User _userModel = new User();

		private Group _groupModel = new Group();

		private Housing _housingModel = new Housing();

		private Cabinet _cabinetModel = new Cabinet();

		private Certificate _certificateModel = new Certificate();

		private IPAddress _ipAddressModel = new IPAddress();

		private DeviceAccount _accountModel = new DeviceAccount();

		private DeviceMovementHistory _deviceMovementHistory = new DeviceMovementHistory();

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

		public DeviceAccount DeviceAccountModel => _accountModel;

		public Cabinet CabinetModel => _cabinetModel;

		public Certificate CertificateModel => _certificateModel;

		public DeviceType DeviceTypeModel => _deviceTypeModel;

		public Group GroupModel => _groupModel;

		public Housing HousingModel => _housingModel;

		public IPAddress IPAddressModel => _ipAddressModel;

		public User UserModel => _userModel;
	}
}
