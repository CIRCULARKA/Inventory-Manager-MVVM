using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class CertificateModelConfiguration : IEntityTypeConfiguration<Certificate>
	{
		public void Configure(EntityTypeBuilder<Certificate> builder)
		{
			builder.HasKey(c => c.ID);
			builder.Property(c => c.ID).UseIdentityColumn();
			builder.Ignore(c => c.State);
		}
	}
}
