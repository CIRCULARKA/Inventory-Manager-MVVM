using InventoryManager.Data;

namespace InventoryManager.Models
{
	public interface IRepository
	{
		protected BaseDbContext DataContext { get; }

		void SaveChanges() => DataContext.SaveChanges();
	}
}
