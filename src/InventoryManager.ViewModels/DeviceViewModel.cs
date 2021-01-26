using Microsoft.EntityFrameworkCore;
using InventoryManager.Commands;
using InventoryManager.Models;
using InventoryManager.Data;
using InventoryManager.Views;
using System.Linq;

namespace InventoryManager.ViewModels
{
	public class DeviceViewModel
	{
		public DeviceViewModel(InventoryManagerDbContext context)
		{
			Data = context;
			OpenAddDeviceViewCommand = new ButtonCommand(
				(obj) =>
				{
					var addDeviceWindow = new AddDeviceView();
					addDeviceWindow.Show();
				}
			);
		}

		public ButtonCommand OpenAddDeviceViewCommand { get; }

		public InventoryManagerDbContext Data { get; }

		public IQueryable<Device> Devices =>
			Data.Devices.Include(d => d.DeviceType).ToList().AsQueryable();

		public Device SelectedDevice { get; set; }

		public string MessageToUser { get; set; }
	}
}
