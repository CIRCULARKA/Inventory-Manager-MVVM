using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class DeviceModelConfiguration : IEntityTypeConfiguration<Device>
	{
		public void Configure(EntityTypeBuilder<Device> builder)
		{
			builder.HasKey(d => d.ID);
			builder.HasIndex(d => d.InventoryNumber).IsUnique();
			builder.Property(d => d.DeviceTypeID).IsRequired();
			builder.Property(d => d.NetworkName).IsRequired();
		}
	}
}
