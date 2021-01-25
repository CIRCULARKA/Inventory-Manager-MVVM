using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class DeviceTypeModelConfiguration : IEntityTypeConfiguration<DeviceType>
	{
		public void Configure(EntityTypeBuilder<DeviceType> builder)
		{
			builder.HasKey(dt => dt.ID);
			builder.Property(dt => dt.ID).UseIdentityColumn();

			builder.Property(dt => dt.Name).IsRequired();

			builder.HasData(
				new DeviceType[]
				{
					new DeviceType { ID = 1, Name = "Персональный компьютер" },
					new DeviceType { ID = 2, Name = "Сервер" },
					new DeviceType { ID = 3, Name = "Коммутатор" }
				}
			);
		}
	}
}
