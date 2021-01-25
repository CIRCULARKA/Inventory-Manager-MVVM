using System.Windows;
using InventoryManager.ViewModels;

namespace InventoryManager.Views
{
	public partial class UserView : ViewBase
	{
		public UserView()
		{
			InitializeComponent();

			DataContext = new DeviceViewModel(Data);
		}
	}
}
