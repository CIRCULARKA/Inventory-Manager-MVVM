using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using InventoryManager.Models;
using InventoryManager.Data;
using System.Linq;

namespace InventoryManager.ViewModels
{
	public class DeviceViewModel
	{
		public DeviceViewModel(InventoryManagerDbContext context)
		{
			Data = context;
		}

		public InventoryManagerDbContext Data { get; }

		public IQueryable<Device> Devices =>
			Data.Devices.Include(d => d.DeviceType).ToList().AsQueryable();

		public Device SelectedDevice { get; set; }

		public string MessageToUser { get; set; }
	}
}
