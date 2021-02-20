// #define RELEASE

using System.Windows;
using InventoryManager.Views;

namespace InventoryManager
{
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs info)
		{
			base.OnStartup(info);

#if RELEASE
			var authWindow = new AuthorizationView();
			authWindow.Show();
#else
			var mainView = new MainView();
			mainView.Show();
#endif
		}
	}
}
