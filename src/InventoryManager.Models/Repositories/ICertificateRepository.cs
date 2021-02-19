using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Models
{
	public interface ICertificateRepository : IRepository
	{
		void AddCertificate(Certificate newCertificate) =>
			DataContext.Certificates.Add(newCertificate);

		void RemoveCertificate(Certificate certificateToRemove) =>
			DataContext.Certificates.Remove(certificateToRemove);

		void UpdateCertificate(Certificate certificateToUpdate) =>
			DataContext.Certificates.Update(certificateToUpdate);

		IEnumerable<Certificate> AllCertificates =>
			DataContext.Certificates.ToList();
	}
}
