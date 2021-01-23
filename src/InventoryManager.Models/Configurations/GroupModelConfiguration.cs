using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.ModelsConfiguration
{
	public class GroupModelConfiguration : IEntityTypeConfiguration<Group>
	{
		public void Configure(EntityTypeBuilder<Group> builder)
		{
			builder.HasKey(p => p.ID);

			builder.HasIndex(p => p.Name).IsUnique();
			builder.Property(p => p.Name).IsRequired();
		}
	}
}
