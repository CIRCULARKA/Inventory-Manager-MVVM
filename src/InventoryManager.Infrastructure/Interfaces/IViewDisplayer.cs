using InventoryManager.Models;

namespace InventoryManager.Infrastructure
{
	public interface IViewDisplayer
	{
		/// <summary>
		/// Shows view for each user group. For example, technician must not see
		/// Users tab in MainView, so this method must not load this tab while creating MainView
		/// </summary>
		void ShowViewForUserGroup(User user);
	}
}
