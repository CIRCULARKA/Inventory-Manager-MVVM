using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class IPAddressModelConfiguration : IEntityTypeConfiguration<IPAddress>
	{
		public void Configure(EntityTypeBuilder<IPAddress> builder)
		{
			builder.HasKey(ia => ia.ID);
			builder.Property(ia => ia.DeviceID).IsRequired(false);
			builder.HasIndex(ia => ia.Address).IsUnique();
			builder.Ignore(ia => ia.IsAvailable);
		}
	}
}
