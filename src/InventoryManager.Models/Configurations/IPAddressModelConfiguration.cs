using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class IPAddressModelConfiguration : IEntityTypeConfiguration<IPAddress>
	{
		public void Configure(EntityTypeBuilder<IPAddress> builder)
		{
			builder.HasKey(ia => ia.ID);
			builder.Property(ia => ia.ID).UseIdentityColumn();
			builder.HasIndex(ia => ia.Address).IsUnique();
			builder.HasData(
					new IPAddress { ID = -1, Address = "192.65.13.1", DeviceID = -1 },
					new IPAddress { ID = -2, Address = "0.0.0.0", DeviceID = -1 }
			);
		}
	}
}
