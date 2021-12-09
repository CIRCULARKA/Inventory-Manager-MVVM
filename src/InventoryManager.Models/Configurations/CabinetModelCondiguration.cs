using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class CabinetModelConfiguration : IEntityTypeConfiguration<Cabinet>
	{
		public void Configure(EntityTypeBuilder<Cabinet> builder)
		{
			builder.HasKey(c => c.ID);
			builder.Property(c => c.Name).IsRequired();
		}
	}
}
