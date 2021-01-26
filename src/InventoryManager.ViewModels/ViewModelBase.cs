using InventoryManager.Models;
using InventoryManager.Data;

namespace InventoryManager.ViewModels
{
	public class ViewModelBase : NotifyingModel
	{
		protected ViewModelBase()
		{
			DataContext = new InventoryManagerDbContext();
		}

		public InventoryManagerDbContext DataContext { get; }
	}
}
