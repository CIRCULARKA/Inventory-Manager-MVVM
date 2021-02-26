using InventoryManager.Data;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public class DefaultCertificateRelatedRepository : ICertificateRelatedRepository
	{
		BaseDbContext DataContext { get; } = new DefaultDbContext();

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
