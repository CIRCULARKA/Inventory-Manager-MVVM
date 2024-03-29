using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class UserModelConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(p => p.LastName).IsRequired();
			builder.Property(p => p.FirstName).IsRequired();
			builder.Property(p => p.MiddleName).IsRequired();
			builder.Property(p => p.Login).IsRequired();
			builder.HasKey(p => p.Login);
			builder.Property(p => p.Password).IsRequired();
			builder.Property(p => p.UserGroupID).IsRequired();
			builder.Ignore(p => p.FullName);
		}
	}
}
