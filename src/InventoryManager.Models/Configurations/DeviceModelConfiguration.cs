using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class DeviceModelConfiguration : IEntityTypeConfiguration<Device>
	{
		public void Configure(EntityTypeBuilder<Device> builder)
		{
			builder.HasKey(d => d.InventoryNumber);
			builder.Property(d => d.DeviceTypeID).IsRequired();
			builder.Property(d => d.NetworkName).IsRequired();

			builder.HasData(
				new Device[]
				{
					new Device
					{
						InventoryNumber = "NSGK530923",
						DeviceTypeID = 1,
						NetworkName = "IVAN-PC",
						DeviceConfigurationID = -1
					}
				}
			);
		}
	}
}
