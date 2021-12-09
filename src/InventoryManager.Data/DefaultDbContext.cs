using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using InventoryManager.Models;
using InventoryManager.Models.Configuration;

namespace InventoryManager.Data
{
	public class DefaultDbContext : BaseDbContext
	{
		public DefaultDbContext()
		{
			Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// optionsBuilder.UseSqlServer(@"Server=(local);Database=InventoryManagerDb;Trusted_Connection=True");

			var connectionString = $"Data Source={Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}{Path.DirectorySeparatorChar}InventoryManagerData.db";
			optionsBuilder.UseSqlite(connectionString);
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
			builder.ApplyConfiguration(new SoftwareTypeConfiguration());
			builder.ApplyConfiguration(new InventoryManager.Models.Configuration.SoftwareConfiguration());

			var usersGroups = new UserGroup[] {
				new UserGroup() { ID = Guid.NewGuid(), Name = "Техник" },
				new UserGroup() { ID = Guid.NewGuid(), Name = "Администратор" },
				new UserGroup() { ID = Guid.NewGuid(), Name = "Суперпользователь" }
			};

			builder.Entity<UserGroup>().HasData(usersGroups);
			builder.Entity<User>().HasData(
				new User() {
					ID = Guid.NewGuid(),
					FirstName = "Иван",
					LastName = "Иванов",
					MiddleName = "Иванович",
					Login = "root",
					Password = "root",
					UserGroupID = usersGroups[2].ID
				}
			);

			var housings = new Housing[] {
				new Housing { ID = Guid.NewGuid(), Name = "Первый корпус" },
				new Housing { ID = Guid.NewGuid(), Name = "Второй корпус" },
				new Housing { ID = Guid.NewGuid(), Name = "N/A" }
			};
			builder.Entity<Housing>().HasData(housings);


			var cabinets = new List<Cabinet>();
			cabinets.Add(new Cabinet { ID = Guid.NewGuid(), Name = "N/A", HousingID = housings[2].ID });

			for (int i = 1; i <= 16; i++)
				cabinets.Add(new Cabinet { ID = Guid.NewGuid(), Name = i.ToString(), HousingID = housings[0].ID });

			for (int i = 1; i <= 12; i++)
				cabinets.Add(new Cabinet { ID = Guid.NewGuid(), Name = "1" + i.ToString(), HousingID = housings[0].ID });

			for (int i = 1; i <= 12; i++)
				cabinets.Add(new Cabinet { ID = Guid.NewGuid(), Name = "2" + i.ToString(), HousingID = housings[0].ID });

			for (int i = 1; i <= 12; i++)
				cabinets.Add(new Cabinet { ID = Guid.NewGuid(), Name = "3" + i.ToString(), HousingID = housings[0].ID });

			for (int i = 1; i <= 12; i++)
				cabinets.Add(new Cabinet { ID = Guid.NewGuid(), Name = "4" + i.ToString(), HousingID = housings[0].ID });

			for (int i = 1; i <= 12; i++)
				cabinets.Add(new Cabinet { ID = Guid.NewGuid(), Name = i.ToString(), HousingID = housings[1].ID });

			for (int i = 1; i <= 12; i++)
				cabinets.Add(new Cabinet { ID = Guid.NewGuid(), Name = "1" + i.ToString(), HousingID = housings[1].ID });

			for (int i = 1; i <= 12; i++)
				cabinets.Add(new Cabinet { ID = Guid.NewGuid(), Name = "2" + i.ToString(), HousingID = housings[1].ID });

			for (int i = 1; i <= 12; i++)
				cabinets.Add(new Cabinet { ID = Guid.NewGuid(), Name = "3" + i.ToString(), HousingID = housings[1].ID });

				builder.Entity<Cabinet>().HasData(cabinets);
		}
	}
}
