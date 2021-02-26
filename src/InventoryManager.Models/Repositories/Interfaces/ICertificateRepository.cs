using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface ICertificateRepository : IRepository
	{
		void AddCertificate(Certificate newCertificate);

		void RemoveCertificate(Certificate certificateToRemove);

		void UpdateCertificate(Certificate certificateToUpdate);

		IEnumerable<Certificate> AllCertificates { get; }
	}
}
