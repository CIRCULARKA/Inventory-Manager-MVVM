using System.Windows.Controls;
using InventoryManager.Models;
using InventoryManager.ViewModels;

namespace InventoryManager.Views
{
	public partial class UsersManagementView : UserControl
	{
		public UsersManagementView()
		{
			InitializeComponent();

			DataContext = new UserViewModel(new DefaultUserRelatedRepository());
		}
	}
}
