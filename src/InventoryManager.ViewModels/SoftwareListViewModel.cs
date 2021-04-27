using InventoryManager.Models;

namespace InventoryManager.ViewModels
{
	public class SoftwareListViewModel : ViewModelBase, ISoftwareListViewModel
	{
		public SoftwareListViewModel(IDeviceRelatedRepository repo)
		{
			Repository = repo;
		}

		private IDeviceRelatedRepository Repository { get; }
	}
}
