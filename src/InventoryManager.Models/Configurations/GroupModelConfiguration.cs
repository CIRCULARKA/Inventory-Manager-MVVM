using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class UserGroupModelConfiguration : IEntityTypeConfiguration<UserGroup>
	{
		public void Configure(EntityTypeBuilder<UserGroup> builder)
		{
			builder.HasKey(p => p.ID);
			builder.HasIndex(p => p.Name).IsUnique();
			builder.Property(p => p.Name).IsRequired();
		}
	}
}
