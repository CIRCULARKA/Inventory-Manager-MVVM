using InventoryManager.Infrastructure;

namespace InventoryManager.ViewModels
{
	interface IWorkinkWithUserViewModel
	{
		IUserSession UserSession { get; }
	}
}
