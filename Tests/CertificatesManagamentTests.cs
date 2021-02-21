using Xunit;
using Moq;
using InventoryManager.Models;
using InventoryManager.ViewModels;
using System;
using System.Collections.Generic;

namespace InventoryManager.Tests
{
	public class CertificatesManagementTests
	{
		private IEnumerable<Certificate> _seedCertificates;

		private IEnumerable<Certificate> BuildSeedCertificates() =>
			new Certificate[]
			{
				new Certificate { Subject = "Microsoft", ID = 1, ValidFrom = DateTime.Now, ValidTo = DateTime.Now },
				new Certificate { Subject = "Apple", ID = 2, ValidFrom = DateTime.Now, ValidTo = DateTime.Now },
				new Certificate { Subject = "Oracle", ID = 3, ValidFrom = DateTime.Now, ValidTo = DateTime.Now },
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

	}
}
