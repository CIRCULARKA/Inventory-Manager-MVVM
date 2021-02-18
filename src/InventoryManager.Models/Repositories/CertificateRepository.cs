using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public class CertificateRepository : RepositoryBase<Certificate>
	{
		public override IEnumerable<Certificate> All =>
			DataContext.Certificates.ToList();
	}
}
