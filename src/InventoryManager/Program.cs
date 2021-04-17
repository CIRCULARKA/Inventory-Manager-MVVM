using System;
using System.Windows;
using Ninject;

namespace InventoryManager
{
	public static class Program
	{
		[STAThread]
		public static void Main()
		{
			try { new App().Run(); }
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
