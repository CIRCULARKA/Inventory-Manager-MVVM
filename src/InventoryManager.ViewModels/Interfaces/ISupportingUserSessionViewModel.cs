using InventoryManager.Infrastructure;

namespace InventoryManager.ViewModels
{
	interface ISupportingUserSessionViewModel
	{
		IUserSession UserSession { get; }
	}
}
