using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.ViewModels;
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

			ShowStartupView(
				new AuthorizationView(),
				new AuthorizationViewModel(
					StandartNinjectKernel.Get<IUserRelatedRepository>()
				)
			);
		}

		public void ShowStartupView(ViewBase viewToShow, ViewModelBase dataContext)
		{
			viewToShow.DataContext = dataContext;
			dataContext.RelatedView = viewToShow;
			viewToShow.Show();
		}
	}
}
