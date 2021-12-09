using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class SoftwareTypeConfiguration : IEntityTypeConfiguration<SoftwareType>
	{
		public void Configure(EntityTypeBuilder<SoftwareType> builder)
		{
			builder.HasKey(st => st.ID);
			builder.Property(st => st.Name).IsRequired();
		}
	}
}
