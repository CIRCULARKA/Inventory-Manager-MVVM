using InventoryManager.Infrastructure;

namespace InventoryManager.ViewModels
{
	interface IWorkingWithUserViewModel
	{
		IUserSession UserSession { get; }
	}
}
