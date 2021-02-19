using InventoryManager.Data;

namespace InventoryManager.Models
{
	public interface IRepository
	{
		IDbContext DataContext { get; }
	}
}
