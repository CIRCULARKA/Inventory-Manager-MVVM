using Microsoft.EntityFrameworkCore;
using InventoryManager.Models;
using InventoryManager.Models.ModelsConfiguration;

namespace InventoryManager.Data
{
	public class InventoryManagerDbContext : DbContext
	{
		public DbSet<Group> Group { get; set; }

		public DbSet<User> User { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(local);Database=InventoryManagerDb;Trusted_Connection=True");
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration<Group>(new GroupModelConfiguration());
			builder.ApplyConfiguration<User>(new UserModelConfiguration());
		}
	}
}
