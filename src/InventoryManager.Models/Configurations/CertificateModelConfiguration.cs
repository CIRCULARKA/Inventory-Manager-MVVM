using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class CertificateModelConfiguration : IEntityTypeConfiguration<Certificate>
	{
		public void Configure(EntityTypeBuilder<Certificate> builder)
		{
		}
	}
}
