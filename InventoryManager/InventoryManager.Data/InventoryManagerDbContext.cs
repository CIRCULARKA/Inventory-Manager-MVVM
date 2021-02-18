using Microsoft.EntityFrameworkCore;
using InventoryManager.Models;
using InventoryManager.Models.Configuration;

namespace InventoryManager.Data
{
	public class InventoryManagerDbContext : DbContext
	{
		public DbSet<Group> Groups { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<DeviceType> DeviceTypes { get; set; }

		public DbSet<Device> Devices { get; set; }

		public DbSet<IPAddress> IPAddresses { get; set; }

		public DbSet<Certificate> Certificates { get; set; }

		public DbSet<Housing> Housings { get; set; }

		public DbSet<Cabinet> Cabinets { get; set; }

		public DbSet<DeviceAccount> DeviceAccounts { get; set; }

		public DbSet<DeviceMovementHistory> DeviceMovementHistory { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(local);Database=InventoryManagerDb;Trusted_Connection=True");
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration<Group>(new GroupModelConfiguration());
			builder.ApplyConfiguration<User>(new UserModelConfiguration());
			builder.ApplyConfiguration<DeviceType>(new DeviceTypeModelConfiguration());
			builder.ApplyConfiguration<Device>(new DeviceModelConfiguration());
			builder.ApplyConfiguration<IPAddress>(new IPAddressModelConfiguration());
			builder.ApplyConfiguration<Certificate>(new CertificateModelConfiguration());
			builder.ApplyConfiguration<Housing>(new HousingModelConfiguration());
			builder.ApplyConfiguration<Cabinet>(new CabinetModelConfiguration());
			builder.ApplyConfiguration<DeviceAccount>(new DeviceAccountModelConfiguration());
			builder.ApplyConfiguration<DeviceMovementHistory>(new DeviceMovementHistoryConfiguration());
		}
	}
}