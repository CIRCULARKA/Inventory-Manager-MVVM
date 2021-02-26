using InventoryManager.Data;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace InventoryManager.Models
{
	public class X509CertificateRelatedRepository : ICertificateRelatedRepository
	{
		BaseDbContext IRepository.DataContext { get; } = null;
	}
}
