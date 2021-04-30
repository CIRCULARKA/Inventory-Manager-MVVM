using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class SoftwareConfiguration : IEntityTypeConfiguration<Software>
	{
		public void Configure(EntityTypeBuilder<Software> builder)
		{
			builder.HasKey(s => s.ID);
			builder.Property(s => s.ID).UseIdentityColumn();
			builder.HasIndex(s => new { s.DeviceID, s.SoftwareTypeID }).IsUnique();
		}
	}
}
