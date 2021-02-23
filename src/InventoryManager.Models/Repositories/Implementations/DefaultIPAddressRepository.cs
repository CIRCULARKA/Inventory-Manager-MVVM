using InventoryManager.Data;

namespace InventoryManager.Models
{
	public class DefaultIPAddressRepository : IIPAddressRepository
	{
		BaseDbContext IRepository.DataContext { get; } = new DefaultDbContext();
	}
}
