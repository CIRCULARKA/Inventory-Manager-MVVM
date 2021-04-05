using InventoryManager.Models;

namespace InventoryManager.Infrastructure
{
	public static class UserRightsBuilder
	{
		public static UserAccessRules GetUserRights(UserAccessRights accessLevel)
		{
			var rules = new UserAccessRules();

			switch (accessLevel)
			{
				case UserAccessRights.Technician:
					rules.AllowAction(UserActions.InspectDevices);
					rules.AllowAction(UserActions.AddDevice);
					rules.AllowAction(UserActions.RemoveDevice);

					rules.AllowAction(UserActions.InspectCertificates);
					break;
				case UserAccessRights.Administrator:
					rules.AllowAction(UserActions.InspectDevices);
					rules.AllowAction(UserActions.AddDevice);
					rules.AllowAction(UserActions.RemoveDevice);

					rules.AllowAction(UserActions.InspectCertificates);

					rules.AllowAction(UserActions.InspectUsers);
					break;
				case UserAccessRights.Superuser:
					rules.AllowEverything();
					break;
			}

			return rules;
		}
	}
}
