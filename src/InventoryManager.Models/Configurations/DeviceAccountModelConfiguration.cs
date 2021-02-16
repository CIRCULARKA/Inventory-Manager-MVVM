using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class DeviceAccountModelConfiguration : IEntityTypeConfiguration<DeviceAccount>
	{
		public void Configure(EntityTypeBuilder<DeviceAccount> builder)
		{
			builder.HasKey(a => a.ID);
			builder.Property(a => a.ID).UseIdentityColumn();
			builder.HasIndex(a => a.Login).IsUnique();
			builder.HasData(
				new DeviceAccount
				{
					ID = -1,
					Login = "Student1",
					Password = "jlsdft324",
					DeviceID = -1
				},
				new DeviceAccount
				{
					ID = -2,
					Login = "Root",
					Password = "lk54Sf",
					DeviceID = -2
				}
			);
		}
	}
}
