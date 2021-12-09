using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class DeviceTypeModelConfiguration : IEntityTypeConfiguration<DeviceType>
	{
		public void Configure(EntityTypeBuilder<DeviceType> builder)
		{
			builder.HasKey(dt => dt.ID);
			builder.Property(dt => dt.Name).IsRequired();
			builder.HasData(
				new DeviceType { ID = Guid.NewGuid(), Name = "Персональный компьютер" },
				new DeviceType { ID = Guid.NewGuid(), Name = "Коммутатор" },
				new DeviceType { ID = Guid.NewGuid(), Name = "Сервер" }
			);
		}
	}
}
