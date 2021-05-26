using InventoryManager.Views;
using InventoryManager.Models;
using InventoryManager.ViewModels;
using InventoryManager.DependencyInjection;
using InventoryManager.UsersAccess;
using System;
using System.Windows;

namespace InventoryManager
{
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs info)
		{
			base.OnStartup(info);

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

			Application.Current.Resources.MergedDictionaries.Add(
				Application.LoadComponent(new Uri("../Styles/Temp.xaml", UriKind.Relative))
					as ResourceDictionary
			);

			Application.Current.Resources.MergedDictionaries.Add(
				Application.LoadComponent(new Uri("../Styles/ComboBoxStyle.xaml", UriKind.Relative))
					as ResourceDictionary
			);

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
