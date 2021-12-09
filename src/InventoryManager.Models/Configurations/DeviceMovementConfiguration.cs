using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class DeviceMovementHistoryNoteConfiguration : IEntityTypeConfiguration<DeviceMovementHistoryNote>
	{
		public void Configure(EntityTypeBuilder<DeviceMovementHistoryNote> builder)
		{
			builder.HasKey(dmh => dmh.ID);
		}
	}
}
