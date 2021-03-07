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
			builder.Ignore(ia => ia.IsAvailable);
			builder.HasData(
					new IPAddress { ID = -1, Address = "192.65.13.1", DeviceID = -1 },
					new IPAddress { ID = -2, Address = "102.1.99.0", DeviceID = -1 },
					new IPAddress { ID = -3, Address = "5.123.33.255", DeviceID = -2 },
					new IPAddress { ID = -4, Address = "55.13.2.1", DeviceID = -2 }
			);
		}
	}
}
