using InventoryManager.Models;
using InventoryManager.Data;

namespace InventoryManager.ViewModels
{
	public class ViewModelBase : NotifyingModel
	{
		public ViewModelBase()
		{
			DataContext = new InventoryManagerDbContext();
		}

		InventoryManagerDbContext DataContext { get; }
	}
}
