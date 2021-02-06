using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManager.Models.Configuration
{
	public class AccountModelConfiguration : IEntityTypeConfiguration<Account>
	{
		public void Configure(EntityTypeBuilder<Account> builder)
		{
			builder.HasKey(a => a.ID);
			builder.Property(a => a.ID).UseIdentityColumn();
			builder.Property(a => a.Login).IsRequired();
			builder.HasData(
				new Account
				{
					ID = -1,
					Login = "AdminIvan",
					Password = "root"
				}
			);
		}
	}
}