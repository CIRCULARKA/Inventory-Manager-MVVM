using Xunit;
using Moq;
using InventoryManager.Models;
using InventoryManager.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.Tests
{
	public class CertificatesManagementTests
	{
		private IEnumerable<Certificate> _seedCertificates;

		private IEnumerable<Certificate> BuildSeedCertificates() =>
			new Certificate[]
			{
				new Certificate { Subject = "Microsoft", ID = Guid.NewGuid(), ExpirationDate = DateTime.Now },
				new Certificate { Subject = "Apple", ID = Guid.NewGuid(), ExpirationDate = DateTime.Now },
				new Certificate { Subject = "Oracle", ID = Guid.NewGuid(), ExpirationDate = DateTime.Now }
			};

		[Fact]
		public void AreCertificatesLoadedToShow()
		{
			// Arrange
			var moq = new Mock<ICertificateRelatedRepository>();
			_seedCertificates = BuildSeedCertificates();
			moq.Setup(c => c.AllCertificates).Returns(_seedCertificates);

			var vm1 = new CertificateViewModel(moq.Object);

			// Act
			var listToShow = vm1.CertificatesToShow;

			// Assert
			Assert.Equal(_seedCertificates, listToShow);
		}

		[Fact]
		public void IsAddedCertificateShowsToUser()
		{
			// Arrange
			var moq = new Mock<ICertificateRelatedRepository>();
			_seedCertificates = BuildSeedCertificates();
			moq.Setup(c => c.AllCertificates).Returns(_seedCertificates);

			var vm1 = new CertificateViewModel(moq.Object);
			var newCertificate = new Certificate { Subject = "Oracle" };
			vm1.InputtedSubject = newCertificate.Subject;

			// Act
			vm1.AddCertificateCommand.Execute(null);

			// Assert
			Assert.NotEmpty(vm1.CertificatesToShow.Where(c => c.Subject == newCertificate.Subject));
		}

		[Fact]
		public void IsRemovedCertificateDoesntShowToUser()
		{
			// Arrange
			var moq = new Mock<ICertificateRelatedRepository>();
			_seedCertificates = BuildSeedCertificates();
			moq.Setup(c => c.AllCertificates).Returns(_seedCertificates);

			var vm1 = new CertificateViewModel(moq.Object);
			var subjectToRemove = "Microsoft";
			vm1.SelectedCertificate = vm1.CertificatesToShow.Single(c => c.Subject == subjectToRemove);

			// Act
			vm1.RemoveCertificateCommand.Execute(null);

			// Assert
			Assert.Empty(vm1.CertificatesToShow.Where(c => c.Subject == subjectToRemove));
		}
	}
}
