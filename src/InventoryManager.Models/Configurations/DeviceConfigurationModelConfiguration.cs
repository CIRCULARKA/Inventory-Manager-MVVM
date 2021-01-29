using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class DeviceConfigurationModelConfiguration : IEntityTypeConfiguration<DeviceConfiguration>
	{
		public void Configure(EntityTypeBuilder<DeviceConfiguration> builder)
		{
			builder.HasKey(dc => dc.DeviceInventoryNumber);
		}
	}
}
