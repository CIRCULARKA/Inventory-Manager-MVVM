using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.ViewModels;
<<<<<<< HEAD
using InventoryManager.Infrastructure;
using InventoryManager.DependencyInjection;
=======
using InventoryManager.UsersAccess;
>>>>>>> 26bb1aa7d3329e5b1d38a5da80c27b2553e762e1
using Ninject;
using System.Windows;

namespace InventoryManager
{
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs info)
		{
			base.OnStartup(info);

<<<<<<< HEAD
			Application.Current.Resources.Clear();

			// Temporary
			Application.Current.Resources.MergedDictionaries.Add(
				Application.LoadComponent(new Uri("../Styles/WindowStyle.xaml", UriKind.Relative))
					as ResourceDictionary
			);

			Application.Current.Resources.MergedDictionaries.Add(
				Application.LoadComponent(new Uri("../Styles/TabControlStyle.xaml", UriKind.Relative))
					as ResourceDictionary
			);

			Application.Current.Resources.MergedDictionaries.Add(
				Application.LoadComponent(new Uri("../Styles/TextStyle.xaml", UriKind.Relative))
					as ResourceDictionary
			);

			Application.Current.Resources.MergedDictionaries.Add(
				Application.LoadComponent(new Uri("../Styles/ListBoxStyle.xaml", UriKind.Relative))
					as ResourceDictionary
			);

			Application.Current.Resources.MergedDictionaries.Add(
				Application.LoadComponent(new Uri("../Styles/ButtonsStyle.xaml", UriKind.Relative))
					as ResourceDictionary
			);

			Application.Current.Resources.MergedDictionaries.Add(
				Application.LoadComponent(new Uri("../Styles/LightThemeColors.xaml", UriKind.Relative))
					as ResourceDictionary
			);

			Application.Current.Resources.MergedDictionaries.Add(
				Application.LoadComponent(new Uri("../Styles/GroupBoxStyle.xaml", UriKind.Relative))
					as ResourceDictionary
			);

			Application.Current.Resources.MergedDictionaries.Add(
				Application.LoadComponent(new Uri("../Styles/CheckBoxStyle.xaml", UriKind.Relative))
					as ResourceDictionary
			);

=======
>>>>>>> 26bb1aa7d3329e5b1d38a5da80c27b2553e762e1
			var authorizationViewModel = new AuthorizationViewModel(
				DependencyResolver.Resolve<IUserRelatedRepository>(),
				DependencyResolver.Resolve<IUserSession>()
			);

			var authorizationView = DependencyResolver.Resolve<IAuthorizationView>()
				as AuthorizationView;

			authorizationView.DataContext = authorizationViewModel;

			authorizationViewModel.RelatedView = authorizationView;

			authorizationView.Show();
		}
	}
}
