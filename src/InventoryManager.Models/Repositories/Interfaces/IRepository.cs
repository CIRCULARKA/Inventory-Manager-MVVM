using InventoryManager.Data;

namespace InventoryManager.Models
{
	public interface IRepository
	{
		BaseDbContext DataContext { get; }

		void SaveChanges() => DataContext.SaveChanges();
	}
}
