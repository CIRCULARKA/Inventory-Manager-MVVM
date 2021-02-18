using InventoryManager.Models;

namespace InventoryManager.ViewModels
{
	public abstract class ViewModelBase : NotifyingModel
	{
		private string _messageToUser;

		private UserRepository _userRepo = new UserRepository();

		private CertificateRepository _certificateRepo = new CertificateRepository();

		private LocationRepository _locationRepo = new LocationRepository();

		private DeviceRepository _deviceRepo = new DeviceRepository();

		protected UserRepository Users => _userRepo;

		protected DeviceRepository Devices => _deviceRepo;

		protected CertificateRepository Certificates => _certificateRepo;

		protected LocationRepository Locations => _locationRepo;

		public string MessageToUser
		{
			get => _messageToUser;
			set
			{
				_messageToUser = value;
				OnPropertyChanged("MessageToUser");
			}
		}
	}
}
