using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.ModelsConfiguration
{
	public class UserModelConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(p => p.ID);

			builder.Property(p => p.LastName).IsRequired();
			builder.Property(p => p.FirstName).IsRequired();
			builder.Property(p => p.MiddleName).IsRequired();
			builder.Property(p => p.Login).IsRequired();
			builder.HasIndex(p => p.Login).IsUnique();
			builder.Property(p => p.Password).IsRequired();
			builder.Property(p => p.UserGroup).IsRequired();
		}
	}
}
