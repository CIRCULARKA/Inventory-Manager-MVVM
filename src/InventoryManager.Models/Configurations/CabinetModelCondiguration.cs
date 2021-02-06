using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class CabinetModelConfiguration : IEntityTypeConfiguration<Cabinet>
	{
		public void Configure(EntityTypeBuilder<Cabinet> builder)
		{
			builder.HasKey(c => c.ID);
			builder.Property(c => c.ID).UseIdentityColumn();
			builder.Property(c => c.HousingID).IsRequired();
			builder.Property(c => c.Name).IsRequired();
			builder.HasIndex(c => c.HousingID).IsUnique();
			builder.HasIndex(c => c.Name).IsUnique();
		}
	}
}
