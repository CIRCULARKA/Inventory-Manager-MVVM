using InventoryManager.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System;

namespace InventoryManager.Models
{
	public class X509CertificateRelatedRepository : ICertificateRelatedRepository
	{
		X509Store DataContext { get; } = new X509Store();

		public void AddCertificate(Certificate newCertificate) =>
			throw new NotImplementedException();

		public void RemoveCertificate(Certificate certificateToRemove) =>
			throw new NotImplementedException();

		public void UpdateCertificate(Certificate certificateToUpdate) =>
			throw new NotImplementedException();

		public IEnumerable<Certificate> AllCertificates
		{
			get
			{
				try
				{
					DataContext.Open(OpenFlags.ReadOnly);
					return DataContext.Certificates.ToList();
				}
				finally { DataContext.Close(); }
			}
		}

		public void SaveChanges() => throw new NotImplementedException();
	}
}
