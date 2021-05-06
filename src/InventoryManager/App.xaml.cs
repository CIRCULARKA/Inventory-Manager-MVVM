using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.ViewModels;
using InventoryManager.Infrastructure;
using Ninject;
using System;
using System.Windows;
using static InventoryManager.DependencyInjection.NinjectKernel;

namespace InventoryManager
{
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs info)
		{
			base.OnStartup(info);

			Application.Current.Resources.Clear();

			Application.Current.Resources.MergedDictionaries.Add(
				Application.LoadComponent(new Uri("../Styles/MainTheme.xaml", UriKind.Relative))
					as ResourceDictionary
			);

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
