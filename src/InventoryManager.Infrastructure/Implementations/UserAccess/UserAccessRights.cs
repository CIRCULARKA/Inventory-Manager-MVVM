namespace InventoryManager.Infrastructure
{
	public enum UserAccessRights
	{
		// Place access rights in order from least authorized to most
		// Access level value must be the same as in DB so check database values before
		// adding new const here
		Technician = 1,
		Administrator,
		Superuser
	}
}
