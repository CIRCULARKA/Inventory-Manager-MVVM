using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class LocationModelConfiguration : IEntityTypeConfiguration<Location>
	{
		public void Configure(EntityTypeBuilder<Location> builder)
		{
			builder.HasKey(l => new { l.CabinetID, l.HousingID });
			builder.Property(l => l.ID).UseIdentityColumn();
			builder.HasData(
				new Location { ID = -1, HousingID = -1, CabinetID = -4 },
				new Location { ID = -2, HousingID = 1, CabinetID = -5 },
				new Location { ID = -3, HousingID = 1, CabinetID = -1 },
				new Location { ID = -4, HousingID = 1, CabinetID = -3 },
				new Location { ID = -5, HousingID = 2, CabinetID = -6 },
				new Location { ID = -6, HousingID = 2, CabinetID = -2 }
			);
		}
	}
}
