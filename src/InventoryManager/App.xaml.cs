using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.ViewModels;
using InventoryManager.UsersAccess;
using Ninject;
using System.Windows;
using static InventoryManager.DependencyInjection.NinjectKernel;

namespace InventoryManager
{
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs info)
		{
			base.OnStartup(info);

			var authorizationViewModel = new AuthorizationViewModel(
				StandartNinjectKernel.Get<IUserRelatedRepository>(),
				StandartNinjectKernel.Get<IUserSession>()
			);

			var authorizationView = new AuthorizationView() {
				DataContext = authorizationViewModel
			};

			authorizationViewModel.RelatedView = authorizationView;

			authorizationView.Show();
		}
	}
}
