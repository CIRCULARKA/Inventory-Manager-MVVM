using InventoryManager.Views;
using InventoryManager.Models;

namespace InventoryManager.Infrastructure
{
	public class ViewDisplayer : IViewDisplayer
	{
		private MainView _mainView;

		public ViewDisplayer(MainView mainView)
		{
			_mainView = mainView;
		}

		public void ShowViewForUserGroup(User user)
		{
			
		}
	}
}
