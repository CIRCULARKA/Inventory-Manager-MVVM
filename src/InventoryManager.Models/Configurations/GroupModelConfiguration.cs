using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class GroupModelConfiguration : IEntityTypeConfiguration<Group>
	{
		public void Configure(EntityTypeBuilder<Group> builder)
		{
			builder.HasKey(p => p.ID);
			builder.Property(p => p.ID).UseIdentityColumn();

			builder.HasIndex(p => p.Name).IsUnique();
			builder.Property(p => p.Name).IsRequired();

			builder.HasData(
				new Group { ID = 1, Name = "Техник" },
				new Group { ID = 2, Name = "Администратор" },
				new Group { ID = 3, Name = "Суперпользователь" }
			);
		}
	}
}
