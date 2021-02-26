using InventoryManager.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace InventoryManager.Models
{
	public class X509CertificateRelatedRepository : ICertificateRelatedRepository
	{
		BaseDbContext DataContext { get; } = null;

		public void AddCertificate(Certificate newCertificate) =>
			DataContext.Certificates.Add(newCertificate);

		public void RemoveCertificate(Certificate certificateToRemove) =>
			DataContext.Certificates.Remove(certificateToRemove);

		public void UpdateCertificate(Certificate certificateToUpdate) =>
			DataContext.Certificates.Update(certificateToUpdate);

		public IEnumerable<Certificate> AllCertificates =>
			DataContext.Certificates.ToList();

		public void SaveChanges() => DataContext.SaveChanges();
	}
}
