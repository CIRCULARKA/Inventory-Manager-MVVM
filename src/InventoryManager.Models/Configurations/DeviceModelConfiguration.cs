using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class DeviceModelConfiguration : IEntityTypeConfiguration<Device>
	{
		public void Configure(EntityTypeBuilder<Device> builder)
		{
			builder.HasKey(d => d.ID);
			builder.Property(d => d.ID).UseIdentityColumn();
			builder.HasIndex(d => d.InventoryNumber).IsUnique();
			builder.Property(d => d.DeviceTypeID).IsRequired();
			builder.Property(d => d.NetworkName).IsRequired();

			builder.HasData(
				new Device
				{
					ID = -1,
					InventoryNumber = "NSGK530923",
					DeviceTypeID = 1,
					NetworkName = "IVAN-PC",
				}
			);
		}
	}
}
