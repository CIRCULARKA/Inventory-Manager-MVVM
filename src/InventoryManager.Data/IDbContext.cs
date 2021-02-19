using Microsoft.EntityFrameworkCore;
using InventoryManager.Models;

namespace InventoryManager.Data
{
	public interface IDbContext
	{
		DbSet<UserGroup> UserGroups { get; set; }

		DbSet<User> Users { get; set; }

		DbSet<DeviceType> DeviceTypes { get; set; }

		DbSet<Device> Devices { get; set; }

		DbSet<IPAddress> IPAddresses { get; set; }

		DbSet<Certificate> Certificates { get; set; }

		DbSet<Housing> Housings { get; set; }

		DbSet<Cabinet> Cabinets { get; set; }

		DbSet<DeviceAccount> DeviceAccounts { get; set; }

		DbSet<DeviceMovementHistoryNote> DeviceMovementHistory { get; set; }
	}
}
