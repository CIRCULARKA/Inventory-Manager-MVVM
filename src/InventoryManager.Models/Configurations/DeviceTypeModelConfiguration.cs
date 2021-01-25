using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class DeviceTypeModelConfiguration : IEntityTypeConfiguration<DeviceType>
	{
		public void Configure(EntityTypeBuilder<DeviceType> builder)
		{
			builder.HasKey(dt => dt.ID);
			builder.HasIndex(dt => dt.ID).IsUnique();
			builder.Property(dt => dt.ID).UseIdentityColumn();

			builder.Property(dt => dt.Name).IsRequired();
		}
	}
}
