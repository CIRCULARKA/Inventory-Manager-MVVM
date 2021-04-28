using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class SoftwareTypeConfiguration : IEntityTypeConfiguration<SoftwareType>
	{
		public void Configure(EntityTypeBuilder<SoftwareType> builder)
		{
			builder.HasKey(st => st.ID);
			builder.Property(st => st.ID).UseIdentityColumn();
			builder.Property(st => st.Name).IsRequired();

			builder.HasData(
				new SoftwareType { ID = -1, Name = "Microsoft Word 2016" },
				new SoftwareType { ID = -2, Name = "Microsoft PowerPoint 2016" },
				new SoftwareType { ID = -3, Name = "Embercadero Rad Studio 7.14" }
			);
		}
	}
}
