using InventoryManager.Data;
using System.Linq;

namespace InventoryManager.Models
{
	public class DefaultDeviceRelatedRepository : IDeviceRelatedRepository
	{
		BaseDbContext IRepository.DataContext { get; } = new DefaultDbContext();
	}
}
