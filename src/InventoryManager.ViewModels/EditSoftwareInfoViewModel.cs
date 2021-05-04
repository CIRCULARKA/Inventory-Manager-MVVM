namespace InventoryManager.ViewModels
{
	public class EditSoftwareInfoViewModel : IEditSoftwareInfoViewModel
	{
		public EditSoftwareInfoViewModel()
		{

		}

		public string Login { get; set; }

		public string Password { get; set;  }

		public string AdditionalInformation { get; set; }
	}
}
