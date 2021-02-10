using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class DeviceCabinetModelConfiguration : IEntityTypeConfiguration<DeviceCabinet>
	{
		public void Configure(EntityTypeBuilder<DeviceCabinet> builder)
		{
			builder.HasKey(dc => new { dc.DeviceID, dc.CabinetID });
			builder.HasData(
				new DeviceCabinet {
					DeviceID = -1,
					CabinetID = -1
				},
				new DeviceCabinet {
					DeviceID = -2,
					CabinetID = -2
				},
				new DeviceCabinet {
					DeviceID = -3,
					CabinetID = -3
				}
			);
		}
	}
}
