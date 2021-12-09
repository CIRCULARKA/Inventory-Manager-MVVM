using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class DeviceAccountModelConfiguration : IEntityTypeConfiguration<DeviceAccount>
	{
		public void Configure(EntityTypeBuilder<DeviceAccount> builder)
		{
			builder.HasKey(a => a.ID);
			builder.HasIndex(a => new { a.Login, a.DeviceID }).IsUnique();
		}
	}
}
