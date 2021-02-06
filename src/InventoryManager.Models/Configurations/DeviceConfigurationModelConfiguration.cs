using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class DeviceConfigurationModelConfiguration : IEntityTypeConfiguration<DeviceConfiguration>
	{
		public void Configure(EntityTypeBuilder<DeviceConfiguration> builder)
		{
			builder.HasKey(dc => dc.ID);
			builder.Property(dc => dc.ID).UseIdentityColumn();
			builder.HasData(
				new DeviceConfiguration
				{
					IPAddressID = -1,
					ID = -1,
					AccountID = -1
				}
			);
		}
	}
}
