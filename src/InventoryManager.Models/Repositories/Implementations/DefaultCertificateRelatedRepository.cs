using InventoryManager.Data;

namespace InventoryManager.Models
{
	public class DefaultCertificateRelatedRepository : ICertificateRelatedRepository
	{
		BaseDbContext IRepository.DataContext { get; } = new DefaultDbContext();
	}
}
