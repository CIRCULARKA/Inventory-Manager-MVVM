using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class UserGroupModelConfiguration : IEntityTypeConfiguration<UserGroup>
	{
		public void Configure(EntityTypeBuilder<UserGroup> builder)
		{
			builder.HasKey(p => p.ID);
			builder.Property(p => p.ID).UseIdentityColumn();
			builder.HasIndex(p => p.Name).IsUnique();
			builder.Property(p => p.Name).IsRequired();
			builder.HasData(
				new UserGroup { ID = 1, Name = "Техник" },
				new UserGroup { ID = 2, Name = "Администратор" },
				new UserGroup { ID = 3, Name = "Суперпользователь" }
			);
		}
	}
}
