using Microsoft.EntityFrameworkCore;
using InventoryManager.Models;
using InventoryManager.Models.ModelsConfiguration;

namespace InventoryManager.Data
{
	public class InventoryManagerDbContext : DbContext
	{
		public DbSet<Group> Group { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// Define connection string
			optionsBuilder.UseSqlServer(@"Server=(local);Database=InventoryManagerDb;Trusted_Connection=True");
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration<Group>(new GroupModelConfiguration());
		}
	}
}
