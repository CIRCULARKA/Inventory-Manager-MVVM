using System.Windows;
using InventoryManager.Views;

namespace InventoryManager
{
    public partial class App : Application
    {
		protected override void OnStartup(StartupEventArgs info)
        {
            base.OnStartup(info);

            // Uncomment in production
            // var authWindow = new AuthorizationView();
            // authWindow.Show();

            // Comment in production
            var mainView = new MainView();
            mainView.Show();
        }
    }
}
