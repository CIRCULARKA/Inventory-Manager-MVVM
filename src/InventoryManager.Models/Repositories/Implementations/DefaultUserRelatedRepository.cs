using InventoryManager.Data;

namespace InventoryManager.Models
{
	public class DefaultUserRelatedRepository : IUserRelatedRepository
	{
		BaseDbContext IRepository.DataContext { get; } = new DefaultDbContext();
	}
}
