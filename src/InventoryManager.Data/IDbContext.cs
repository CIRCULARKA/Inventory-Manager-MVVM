using Microsoft.EntityFrameworkCore;
using InventoryManager.Models;

namespace InventoryManager.Data
{
	public abstract class BaseDbContext : DbContext
	{
		public DbSet<UserGroup> UserGroups { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<DeviceType> DeviceTypes { get; set; }

		public DbSet<Device> Devices { get; set; }

		public DbSet<IPAddress> IPAddresses { get; set; }

		public DbSet<Certificate> Certificates { get; set; }

		public DbSet<Housing> Housings { get; set; }

		public DbSet<Cabinet> Cabinets { get; set; }

		public DbSet<DeviceAccount> DeviceAccounts { get; set; }

		public DbSet<DeviceMovementHistoryNote> DeviceMovementHistory { get; set; }
	}
}
