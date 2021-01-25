using System.Windows;
using InventoryManager.Data;

namespace InventoryManager.Views
{
	public class ViewBase : Window
	{
		public InventoryManagerDbContext Data { get; }

		public ViewBase()
		{
			Data = new InventoryManagerDbContext();
		}
	}
}
