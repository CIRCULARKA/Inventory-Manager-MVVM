using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class DeviceMovementHistoryConfiguration : IEntityTypeConfiguration<DeviceMovementHistory>
	{
		public void Configure(EntityTypeBuilder<DeviceMovementHistory> builder)
		{
			builder.HasKey(dmh => new { dmh.ID, dmh.TargetCabinetID });
			builder.Property(dmh => dmh.ID).UseIdentityColumn();
		}
	}
}
