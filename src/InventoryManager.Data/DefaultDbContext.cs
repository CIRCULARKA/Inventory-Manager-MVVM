using Microsoft.EntityFrameworkCore;
using InventoryManager.Models;
using InventoryManager.Models.Configuration;

namespace InventoryManager.Data
{
	public class DefaultDbContext : BaseDbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(local);Database=InventoryManagerDb;Trusted_Connection=True");
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration<UserGroup>(new UserGroupModelConfiguration());
			builder.ApplyConfiguration<User>(new UserModelConfiguration());
			builder.ApplyConfiguration<DeviceType>(new DeviceTypeModelConfiguration());
			builder.ApplyConfiguration<Device>(new DeviceModelConfiguration());
			builder.ApplyConfiguration<IPAddress>(new IPAddressModelConfiguration());
			builder.ApplyConfiguration<Certificate>(new CertificateModelConfiguration());
			builder.ApplyConfiguration<Housing>(new HousingModelConfiguration());
			builder.ApplyConfiguration<Cabinet>(new CabinetModelConfiguration());
			builder.ApplyConfiguration<DeviceAccount>(new DeviceAccountModelConfiguration());
			builder.ApplyConfiguration<DeviceMovementHistoryNote>(new DeviceMovementHistoryNoteConfiguration());
		}
	}
}
