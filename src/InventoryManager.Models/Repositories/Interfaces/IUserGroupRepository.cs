using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface IUserGroupRepository : IRepository
	{
		void AddUserGroup(UserGroup newGroup);

		void RemoveUserGroup(UserGroup groupToRemove);

		void UpdateUserGroup(UserGroup groupToUpdate);

		IEnumerable<UserGroup> AllUserGroups { get; }
	}
}
