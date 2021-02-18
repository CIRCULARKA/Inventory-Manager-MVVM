using System;
using System.Windows;

namespace InventoryManager
{
	public static class Program
	{
		[STAThread]
		public static void Main()
		{
			try
			{
				var app = new App();
				app.Run();
			}
			catch (Exception e)
			{
				MessageBox.Show(
					e.Message,
					"Необработанная ошибка",
					MessageBoxButton.OK,
					MessageBoxImage.Error
				);
			}
		}
	}
}
