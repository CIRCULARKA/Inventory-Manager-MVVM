using InventoryManager.Data;

namespace InventoryManager.Models
{
	public class DefaultUserRelatedRepository : IUserRelatedRepository
	{
		// Need to hide this property somehow
		public BaseDbContext DataContext { get; }

		public DefaultUserRelatedRepository()
		{
			DataContext = new DefaultDbContext();
		}
	}
}
