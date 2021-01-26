using InventoryManager.Models;
using InventoryManager.Data;

namespace InventoryManager.ViewModels
{
	public class ViewModelBase : NotifyingModel
	{
		protected ViewModelBase()
		{
			Data = new InventoryManagerDbContext();
		}

		InventoryManagerDbContext Data { get; }
	}
}
