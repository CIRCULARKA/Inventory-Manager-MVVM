using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class HousingCabinetModelConfiguration : IEntityTypeConfiguration<HousingCabinet>
	{
		public void Configure(EntityTypeBuilder<HousingCabinet> builder)
		{
			builder.HasKey(hc => new { hc.HousingID, hc.CabinetID });
			builder.HasData(
				new HousingCabinet { HousingID = -1, CabinetID = -4 },
				new HousingCabinet { HousingID = 1, CabinetID = -5 },
				new HousingCabinet { HousingID = 1, CabinetID = -1 },
				new HousingCabinet { HousingID = 1, CabinetID = -3 },
				new HousingCabinet { HousingID = 2, CabinetID = -6 },
				new HousingCabinet { HousingID = 2, CabinetID = -2 }
			);
		}
	}
}
