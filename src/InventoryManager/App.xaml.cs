using System.Windows;
using InventoryManager.Views;

namespace InventoryManager
{
    public partial class App : Application
    {
		protected override void OnStartup(StartupEventArgs info)
        {
            base.OnStartup(info);

            var authWindow = new AuthorizationView();
            authWindow.Show();
        }
    }
}
